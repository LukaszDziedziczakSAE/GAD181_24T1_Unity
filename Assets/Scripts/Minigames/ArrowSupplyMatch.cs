using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ArrowSupplyMatch : MinigameMatch
{
    [SerializeField] float matchLength = 60;
    [field: SerializeField] public float EnemySpeed { get; private set; } = 1;

    [SerializeField] GameObject selectionIndicatorPrefab;

    [field: SerializeField] public Transform[] PickupLocations;

    [field: SerializeField] public Transform[] DeliveryLocations;
    public float MatchTimeRemaining => matchLength - MatchTime;

    [SerializeField] int highDamagePointAward;

    [SerializeField] int normalDamagePointAward;

    [SerializeField] int lowDamagePointAward;

    [field: SerializeField] public EnemyDamage[] EnemyDamages { get; private set; } = new EnemyDamage[0];

    protected override void PrematchStart()
    {
        base.PrematchStart();
    }

    protected override void PrematchTick()
    {
        base.PrematchTick();
    }

    protected override void MatchStart()
    {
        foreach (Character character in Compeditors)
        {
            character.SetNewState(new CS_ArrowSupply_Locomotion(character));
        }

        foreach (Character archer in Archers)
        {
            archer.SetNewState(new CS_ArrowSupply_ArcherWaiting(archer));
        }

        Game.UI.MatchStatus.gameObject.SetActive(true);
    }

    protected override void MatchTick()
    {
        base.MatchTick();

        if (MatchTimeRemaining <= 0)
        {
            Mode = EState.postMatch;
        }
    }

    protected override void PostMatchStart()
    {
        base.PostMatchStart();

        DestroyAllEnemies();
    }

    private void DestroyAllEnemies()
    {
        Character[] enemies = AS_Enemies;

        foreach (Character enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    public void ShowTouchIndicator(Vector3 position)
    {
        GameObject selectionIndicator = Instantiate(selectionIndicatorPrefab, position, Quaternion.identity);
    }

    public Character[] Archers
    {
        get
        {
            List<Character> archers = new List<Character>();

            Character[] inLevel = FindObjectsOfType<Character>();

            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex == 101) archers.Add(character);
            }

            return archers.ToArray();
        }
    }

    public Character[] AS_Enemies
    {
        get
        {
            List<Character> archers = new List<Character>();

            Character[] inLevel = FindObjectsOfType<Character>();

            foreach (Character character in inLevel)
            {
                if (character.PlayerIndex == 102) archers.Add(character);
            }

            return archers.ToArray();
        }
    }

    [System.Serializable]
    public class EnemyDamage
    {
        public CharacterModel.Config CharacterType;

        public int NormalDamageMultiplier;

        public int FireDamageMultiplier;

        public int IceDamageMultiplier;

        public int DamgeByArrowType(ArrowSupply_Arrow.EType arrowType)
        {
            switch(arrowType)
            {
                case ArrowSupply_Arrow.EType.normal:
                    return NormalDamageMultiplier;

                case ArrowSupply_Arrow.EType.fire: 
                    return FireDamageMultiplier;

                case ArrowSupply_Arrow.EType.ice: 
                    return IceDamageMultiplier;

                default: return 0;
            }
        }
    }

    public int DamageByType(CharacterModel.EVariant variant, ArrowSupply_Arrow.EType arrowType)
    {
        foreach (EnemyDamage enemyDamage in EnemyDamages)
        {
            if (enemyDamage.CharacterType.Variant == variant)
            {
                return enemyDamage.DamgeByArrowType(arrowType);
            }
        }
        return -1;
    }

    public int PointsByDamage(int damage)
    {
        switch(damage)
        {
            case 4: return highDamagePointAward;

            case 2: return normalDamagePointAward;

            case 1: return lowDamagePointAward;

            default: return 0;
        }
    }
}
