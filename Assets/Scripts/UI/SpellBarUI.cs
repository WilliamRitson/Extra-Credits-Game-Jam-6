using System;
using System.Linq;
using UnityEngine;

public class SpellBarUI : MonoBehaviour
{
    public GameObject spellPanelPrefab;
    public Player player;

    private Spell[] spells;
    public float hotkeyDelayTime = 1.0f;
    private float delay;
    


    private void Start()
    {
        spells = player.gameObject.GetComponents<Spell>()
            .OrderBy(spell => spell.manaCost).ToArray();
        foreach (var spell in spells)
        {
            CreateSpellPanel(spell);
        }
    }

    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        for (var i = 0; i < spells.Length; i ++)
        {
            if (!Input.GetButton($"Spell{i + 1}")) continue;
            player.AttemptToCastSpell(spells[i]);
            delay = hotkeyDelayTime;
        }
    }

    private void CreateSpellPanel(Spell spell)
    {
        var panel = Instantiate(spellPanelPrefab, transform);
        panel.GetComponent<SpellPanelUI>().BindSpell(player, spell);
    }
}
