using Godot;
using System;

public partial class MusicControl : HSlider
{
	public void alterarVolume(float value)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), Mathf.LinearToDb(value));
	}
}
