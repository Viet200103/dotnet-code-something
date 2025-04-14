namespace CSharpTryToLearn.DesignPattern.StructuralPattern.Adapter;

/**
 * Scenario: Integrating a Legacy Inventory System with a Modern E-Commerce Platform
 * Expects product IDs in a different format (e.g., "SKU123" instead of "PROD-123").
 * Returns stock data in a flat, outdated structure.
 * Throws exceptions for invalid inputs instead of using modern error handling.
 */
public class LegacyECommerce
{
    public record ProductVariant(string Size, string Color);
    public record Product(string ProductId, List<ProductVariant> Variants, int Quantity);

    public interface IInventoryService
    {
        Task<Product> GetStockAsync(string productId);
        Task<bool> UpdateStockAsync(string productId, int quantity, ProductVariant? variant = null);
    }
    
    public class LegacyStockItem
    {
        public string ItemCode { get; set; }
        public int StockLevel { get; set; }
        public string Attributes { get; set; }
    }
    
    public class OldInventorySystem
    {

        public LegacyStockItem GetInventory(string itemCode)
        {
            if (String.IsNullOrEmpty(itemCode) || !itemCode.StartsWith("SKU"))
            {
                throw new ArgumentException("Invalid item code");
            }

            if (Random.Shared.Next(0, 10) == 0)
            {
                throw new TimeoutException("Legacy system timed out");
            }
            
            return new LegacyStockItem
            {
                ItemCode = itemCode,
                StockLevel = 100, // Dummy data
                Attributes = "Size:M;Color:Blue" // Dummy attributes
            };
        }
        
        public bool SetInventory(string itemCode, int newStock)
        {
            if (string.IsNullOrEmpty(itemCode) || !itemCode.StartsWith("SKU"))
            {
                throw new ArgumentException("Invalid item code");
            }

            if (newStock < 0)
            {
                throw new ArgumentException("Stock cannot be negative");
            }

            // Simulate flaky behavior
            if (Random.Shared.Next(0, 10) == 0)
            {
                throw new TimeoutException("Legacy system timed out");
            }

            Console.WriteLine($"Legacy: Updated {itemCode} to {newStock}");
            return true;
        }
    }
    
    public class InventoryAdapter : IInventoryService
    {
        private readonly OldInventorySystem _legacySystem;
        private readonly ILogger _logger; // Simulated logger
        private readonly int _maxRetries = 3;

        public InventoryAdapter(OldInventorySystem legacySystem, ILogger logger)
        {
            _legacySystem = legacySystem;
            _logger = logger;
        }
        
        public async Task<Product> GetStockAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                _logger.LogError("Product ID is empty");
                throw new ArgumentException("Product ID cannot be empty");
            }

            string itemCode = ConvertToLegacyItemCode(productId);
            LegacyStockItem legacyItem = await RetryAsync(() => _legacySystem.GetInventory(itemCode), $"GetInventory for {itemCode}");

            return MapToProduct(legacyItem);
        }

        public async Task<bool> UpdateStockAsync(string productId, int quantity, ProductVariant? variant = null)
        {
            if (string.IsNullOrEmpty(productId))
            {
                _logger.LogError("Product ID is empty");
                throw new ArgumentException("Product ID cannot be empty");
            }

            if (quantity < 0)
            {
                _logger.LogError("Quantity cannot be negative");
                throw new ArgumentException("Quantity cannot be negative");
            }

            string itemCode = ConvertToLegacyItemCode(productId);
            // Note: Legacy system doesn't support variants, so we ignore variant for simplicity
            bool result = await RetryAsync(() => _legacySystem.SetInventory(itemCode, quantity), $"SetInventory for {itemCode}");

            _logger.LogInformation($"Updated stock for {productId} to {quantity}");
            return result;
        }

        private Product MapToProduct(LegacyStockItem legacyItem)
        {
            var variants = new List<ProductVariant>();
            if (!string.IsNullOrEmpty(legacyItem.Attributes))
            {
                var attributes = legacyItem.Attributes.Split(';')
                    .Select(attr => attr.Split(':'))
                    .Where(parts => parts.Length == 2)
                    .ToDictionary(parts => parts[0], parts => parts[1]);

                if (attributes.ContainsKey("Size") && attributes.ContainsKey("Color"))
                {
                    variants.Add(new ProductVariant(attributes["Size"], attributes["Color"]));
                }
            }

            string productId = ConvertFromLegacyItemCode(legacyItem.ItemCode);
            return new Product(productId, variants, legacyItem.StockLevel);
        }

        private string ConvertToLegacyItemCode(string productId)
        {
            if (productId.StartsWith("PROD-"))
            {
                return "SKU" + productId.Substring(5);
            }

            return productId;
        }
        
        private string ConvertFromLegacyItemCode(string itemCode)
        {
            // Legacy: "SKU123" -> Modern: "PROD-123"
            if (itemCode.StartsWith("SKU"))
            {
                return "PROD-" + itemCode.Substring(3);
            }
            return itemCode;
        }
        
        private async Task<T> RetryAsync<T>(Func<T> operation, string operationName)
        {
            int attempt = 0;
            while (true)
            {
                try
                {
                    return await Task.Run(operation); 
                }
                catch (TimeoutException ex)
                {
                    attempt++;
                    if (attempt >= _maxRetries)
                    {
                        _logger.LogError($"Failed {operationName} after {attempt} attempts: {ex.Message}");
                        throw;
                    }
                    _logger.LogWarning($"Retrying {operationName} (attempt {attempt}/{_maxRetries})");
                    await Task.Delay(100 * attempt); // Exponential backoff simplified
                }
                catch (ArgumentException ex)
                {
                    _logger.LogError($"Invalid input for {operationName}: {ex.Message}");
                    throw;
                }
            }
        }
    }
    
    public interface ILogger
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
    
    public class ConsoleLogger : ILogger
    {
        public void LogInformation(string message) => Console.WriteLine($"INFO: {message}");
        public void LogWarning(string message) => Console.WriteLine($"WARN: {message}");
        public void LogError(string message) => Console.WriteLine($"ERROR: {message}");
    }
    
    public class ECommerceProgram
    {
        public static void Run()
        {
            
            ECommerceProgram program = new ECommerceProgram();
            Task.WaitAll(program.Execute());
        }

        public async Task Execute()
        {
            OldInventorySystem legacySystem = new OldInventorySystem();
            ILogger logger = new ConsoleLogger();
            IInventoryService inventoryService = new InventoryAdapter(legacySystem, logger);

            try
            {
                // Get stock
                Product product = await inventoryService.GetStockAsync("PROD-123");
                Console.WriteLine($"Product: {product.ProductId}, Quantity: {product.Quantity}, Variants: {string.Join(", ", product.Variants)}");

                // Update stock
                bool updated = await inventoryService.UpdateStockAsync("PROD-123", 50);
                Console.WriteLine($"Stock update successful: {updated}");

                // Test invalid input
                await inventoryService.GetStockAsync("");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}