using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SpellPanelUI : MonoBehaviour
{
    private Spell spell;
    private Player caster;

    public TextMeshProUGUI title;
    public TextMeshProUGUI tooltipText;

    public Image icon;
    public Button button;
    
    public void OnClick()
    {
        if (caster == null) return;
        caster.AttemptToCastSpell(spell);
    }

    public void BindSpell(Player newCaster, Spell newSpell)
    {
        caster = newCaster;
        spell = newSpell;
        icon.sprite = newSpell.spellIcon;
        title.text = $"{newSpell.spellName} ({newSpell.manaCost})";
        tooltipText.text = newSpell.tooltip;
    }

    private void Update()
    {
        button.interactable = caster != null && caster.CanCastSpell(spell);
    }
    

}
