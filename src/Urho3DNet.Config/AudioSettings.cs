namespace Urho3DNet.Config
{
    public class AudioSettings
    {
        private readonly Context _context;

        static readonly string SOUND_MASTER = "Master";
        static readonly string SOUND_EFFECT = "Effect";
        static readonly string SOUND_AMBIENT = "Ambient";
        static readonly string SOUND_VOICE = "Voice";
        static readonly string SOUND_MUSIC = "Music";

        public AudioSettings(Context context)
        {
            _context = context;
        }

        public float MasterGain
        {
            get => _context.Audio.GetMasterGain(SOUND_MASTER);
            set => _context.Audio.SetMasterGain(SOUND_MASTER, value);
        }

        public float EffectGain
        {
            get => _context.Audio.GetMasterGain(SOUND_EFFECT);
            set => _context.Audio.SetMasterGain(SOUND_EFFECT, value);
        }

        public float AmbientGain
        {
            get => _context.Audio.GetMasterGain(SOUND_AMBIENT);
            set => _context.Audio.SetMasterGain(SOUND_AMBIENT, value);
        }
        public float VoiceGain
        {
            get => _context.Audio.GetMasterGain(SOUND_VOICE);
            set => _context.Audio.SetMasterGain(SOUND_VOICE, value);
        }
        public float MusicGain
        {
            get => _context.Audio.GetMasterGain(SOUND_MUSIC);
            set => _context.Audio.SetMasterGain(SOUND_MUSIC, value);
        }
    }
}