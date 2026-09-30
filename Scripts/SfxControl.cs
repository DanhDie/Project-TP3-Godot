using Godot;
using System;

public partial class SfxControl : HSlider
{
    public void alterarVolume(float value)
    {
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SFX"), Mathf.LinearToDb(value));

    }
}
