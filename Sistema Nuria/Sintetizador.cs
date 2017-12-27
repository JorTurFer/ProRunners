using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;

namespace Sistema_Nuria
{
    static class Sintetizador
    {
        static SpeechSynthesizer m_Sintetizador = new SpeechSynthesizer();

        static Sintetizador()
        {            
            m_Sintetizador.SetOutputToDefaultAudioDevice();
        }

        public static void DecirAsync(string strFrase)
        {
            m_Sintetizador.SpeakAsync(strFrase);
        }
        public static void Decir(string strFrase)
        {
            m_Sintetizador.Speak(strFrase);
        }
    }
}
