namespace CSharpTryToLearn.DesignPattern.StructuralPattern.Adapter;

public class LegacyAudio
{
 
    public interface IMediaPlayer
    {
        void Play(string audioType, string fileName);
    }
    
    public class LegacyAudioPlayer
    {
        public void PlayWav(string fileName)
        {
            Console.WriteLine($"Playing WAV file: {fileName}");
        }
    }

    private class AudioAdapter(LegacyAudioPlayer legacyAudioPlayer) : IMediaPlayer
    {
        public void Play(string audioType, string fileName)
        {
            if (audioType.Equals("wav", StringComparison.OrdinalIgnoreCase))
            {
                legacyAudioPlayer.PlayWav(fileName);
            }
            else
            {
                Console.WriteLine($"Unsupported format: {audioType}. Only WAV is supported.");
            }
        }
    }
    
    public static class AudioProgram
    {

        public static void Run()
        {
            LegacyAudioPlayer legacyPlayer = new LegacyAudioPlayer();
            IMediaPlayer player = new AudioAdapter(legacyPlayer);

            player.Play("wav", "song1.wav"); 
            player.Play("mp3", "song2.mp3");  
        }
    }
}