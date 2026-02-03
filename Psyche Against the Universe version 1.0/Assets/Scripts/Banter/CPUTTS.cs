using UnityEngine;
using System.Runtime.InteropServices;
/// <summary>
/// General CPU Text to speech manager for speaking the CPU banter lines.
/// Utilizes a wrapper structure to envelop the open source native TTS file.
/// Native 
/// 
/// hold this for now. May not be required
/// 
/// </summary>
public static class CPUTTS
{
    public static void CPUSpeak(string text)
    {
#if UNITY_WEBGL && UNITY_EDITOR
        SpeakWebGL(text);
#else   
        SpeakNative(text);
#endif
    }

    private static void SpeakNative(string text)
    {
        //default is windows. May need to account for mac as well. 
       // System.Speech.Syntheseis.SpeechSynthesizer synth = new System.Speech.Synthesis.SpeechSynthesizer();
       // synth.SpeakAsync(text);
    }

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void WebGL_Speak(string text);

    private static void SpeakWebGL(string text)
    {
        WebGL_Speak(text);
    }

}
