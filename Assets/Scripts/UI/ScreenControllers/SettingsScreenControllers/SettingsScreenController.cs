using System.Collections.Generic;
using UnityEngine.UI;

public class SettingsScreenController : ModularScreenController
{
    public Dropdown SelectionDropdown;
    public List<string> Options;

    public override void Start()
    {
        base.Start();
        SelectionDropdown.AddOptions(Options);
    }
}
