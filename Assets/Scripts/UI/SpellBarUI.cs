using System;
using UnityEngine;

public class SpellBarUI : MonoBehaviour
{
    public GameObject spellPanelPrefab;
    public Player player;

    private void Start()
    {
        var spells = player.gameObject.GetComponents<Spell>();
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
