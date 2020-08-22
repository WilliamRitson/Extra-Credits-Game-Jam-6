using System;
using System.Linq;
using UnityEngine;

public class SpellBarUI : MonoBehaviour
{
    public GameObject spellPanelPrefab;
    public Player player;

    private void Start()
    {
        var spells = player.gameObject.GetComponents<Spell>()
            .OrderBy(spell => spell.manaCost);
        foreach (var spell in spells)
        {
            CreateSpellPanel(spell);
        }
    }

    private void CreateSpellPanel(Spell spell)
    {
        var panel = Instantiate(spellPanelPrefab, transform);
        panel.GetComponent<SpellPanelUI>().BindSpell(player, spell);
    }
}
