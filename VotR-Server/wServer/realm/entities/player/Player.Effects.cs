﻿using System;
using common.resources;

namespace wServer.realm.entities
{
    partial class Player
    {
        float _healing;
        float _healing2;
        float _bleeding;
        float _surgeDepletion;
        float _surgeDepletion2;
        int _newbieTime;
        int _canTpCooldownTime;
        int protectionDamage = 0;
        bool isSurgeGone;
        bool surgewither;
        void HandleEffects(RealmTime time)
        {
                if (CheckAxe())
                {
                    Stats.Boost.ActivateBoost[3].Push(10, false);
                    Stats.ReCalculateValues();
                }
                else
                {
                    Stats.Boost.ActivateBoost[3].Pop(10, false);
                    Stats.ReCalculateValues();
                }
            if (CheckSunMoon())
            {
                Stats.Boost.ActivateBoost[1].Push(100, false);
                Stats.ReCalculateValues();
            }
            else
            {
                Stats.Boost.ActivateBoost[1].Pop(100, false);
                Stats.ReCalculateValues();
            }
            if (CheckAnubis())
            {
                Stats.Boost.ActivateBoost[1].Push(60, false);
                Stats.ReCalculateValues();
            }
            else
            {
                Stats.Boost.ActivateBoost[1].Pop(60, false);
                Stats.ReCalculateValues();
            }
            if (CheckMocking())
            {
                ApplyConditionEffect(ConditionEffectIndex.Relentless);
            }
            else
            {
                ApplyConditionEffect(ConditionEffectIndex.Relentless, 0);
            }
            if (CheckForce())
            {
                ApplyConditionEffect(ConditionEffectIndex.ArmorBreakImmune);
            }
            else
            {
                ApplyConditionEffect(ConditionEffectIndex.ArmorBreakImmune, 0);
            }
            if (CheckRoyal())
            {
                ApplyConditionEffect(ConditionEffectIndex.HealthRecovery);
            }
            else
            {
                ApplyConditionEffect(ConditionEffectIndex.HealthRecovery, 0);
            }
            if (CheckResistance())
            {
                ApplyConditionEffect(ConditionEffectIndex.SlowedImmune);
            }
            else
            {
                ApplyConditionEffect(ConditionEffectIndex.SlowedImmune, 0);
            }

            if (CheckAegis())
            {
                ApplyConditionEffect(ConditionEffectIndex.Vengeance);
            }
            else
            {
                ApplyConditionEffect(ConditionEffectIndex.Vengeance, 0);
            }

            if (CheckGuilded())
            {
                ApplyConditionEffect(ConditionEffectIndex.Alliance);
            }
            else
            {
                ApplyConditionEffect(ConditionEffectIndex.Alliance, 0);
            }

            ProtectionMax = (int)(((Math.Pow(Stats[11], 2)) * 0.05) + (Stats[0] / 50))+10;
            Protection =    (int)(((Math.Pow(Stats[11], 2)) * 0.05) + (Stats[0] / 50))+10-protectionDamage;
            if(Protection > 0)
            {
                ApplyConditionEffect(ConditionEffectIndex.Protected);
            }
            else
            {
                ApplyConditionEffect(ConditionEffectIndex.Protected, 0);

            }
            if(Protection < 0)
            {
            Protection = 0;
            }
            if(Surge == 100)
            {
                protectionDamage = 0;
            }
            MainLegendaryPassives();
            if (SurgeCounter == 1)
            {
                Surge = 0;
            }
            if (_client.Account.Hidden && !HasConditionEffect(ConditionEffects.Hidden))
            {
                ApplyConditionEffect(ConditionEffectIndex.Hidden);
                ApplyConditionEffect(ConditionEffectIndex.Invincible);
                Manager.Clients[Client].Hidden = true;
            }

            if (Muted && !HasConditionEffect(ConditionEffects.Muted))
                ApplyConditionEffect(ConditionEffectIndex.Muted);

            if (HasConditionEffect(ConditionEffects.Healing) && !HasConditionEffect(ConditionEffects.Sick))
            {
                if (_healing > 1)
                {
                    HP = Math.Min(Stats[0], HP + (int)_healing);
                    _healing -= (int)_healing;
                }
                _healing += 28 * (time.ElaspedMsDelta / 1000f);
            }

            if (HasConditionEffect(ConditionEffects.HealthRecovery) && !HasConditionEffect(ConditionEffects.Sick))
            {
                if (_healing2 > 1)
                {
                    HP = Math.Min(Stats[0], HP + (int)_healing2);
                    _healing2 -= (int)_healing2;
                }
                _healing2 += 36 * (time.ElaspedMsDelta / 1000f);
            }

            if (HasConditionEffect(ConditionEffects.Quiet) && MP > 0)
                MP = 0;
            
            if (HasConditionEffect(ConditionEffects.Bleeding) && HP > 1)
            {
                if (_bleeding > 1)
                {
                    HP -= (int)_bleeding;
                    if (HP < 1)
                        HP = 1;
                    _bleeding -= (int)_bleeding;
                }
                _bleeding += 28 * (time.ElaspedMsDelta / 1000f);
            }
            if (isSurgeGone)
            {
                if (_surgeDepletion > 1)
                {
                    SurgeCounter -= (int)_surgeDepletion;
                    if (SurgeCounter < 1)
                        SurgeCounter = 0;
                    _surgeDepletion -= (int)_surgeDepletion;
                    if (SurgeCounter == 0)
                    {
                        isSurgeGone = false;
                        surgewither = true;
                    }
                }
                _surgeDepletion += 6 * (time.ElaspedMsDelta / 1000f);
            }
            if (surgewither)
            {
                if (_surgeDepletion2 > 1)
                {
                    Surge -= (int)_surgeDepletion2;
                    if (Surge < 1)
                        Surge = 0;
                    _surgeDepletion2 -= (int)_surgeDepletion2;
                }
                _surgeDepletion2 += 3 * (time.ElaspedMsDelta / 1000f);
            }

            if (HasConditionEffect(ConditionEffects.NinjaSpeedy))
            {
                MP = Math.Max(0, (int)(MP - 10 * time.ElaspedMsDelta / 1000f));

                if (MP == 0)
                    ApplyConditionEffect(ConditionEffectIndex.NinjaSpeedy, 0);
            }
            if (HasConditionEffect(ConditionEffects.Protected))
            {
                ApplyConditionEffect(ConditionEffectIndex.ParalyzeImmune);
                ApplyConditionEffect(ConditionEffectIndex.StunImmune);
            }
            else
            {
                ApplyConditionEffect(ConditionEffectIndex.ParalyzeImmune, 0);
                ApplyConditionEffect(ConditionEffectIndex.StunImmune, 0);
            }
            if (HasConditionEffect(ConditionEffects.SamuraiBerserk))
            {
                MP = Math.Max(0, (int)(MP - 10 * time.ElaspedMsDelta / 1000f));

                if (MP == 0)
                    ApplyConditionEffect(ConditionEffectIndex.SamuraiBerserk, 0);
            }

            if (HasConditionEffect(ConditionEffects.DrakzixCharging))
            {

                Owner.Timers.Add(new WorldTimer(100, (w, t) =>
                {
                    HP -= 10;
                    DrainedHP += 1;
                }));
            }

            if (_newbieTime > 0)
            {
                _newbieTime -= time.ElaspedMsDelta;
                if (_newbieTime < 0) 
                    _newbieTime = 0;
            }

            if (_canTpCooldownTime > 0)
            {
                _canTpCooldownTime -= time.ElaspedMsDelta;
                if (_canTpCooldownTime < 0)
                    _canTpCooldownTime = 0;
            }
        }

        bool CanHpRegen()
        {
            if (HasConditionEffect(ConditionEffects.Sick))
                return false;
            if (HasConditionEffect(ConditionEffects.Bleeding))
                return false;
            return true;
        }

        bool CanMpRegen()
        {
            if (HasConditionEffect(ConditionEffects.Quiet) ||
                    HasConditionEffect(ConditionEffects.NinjaSpeedy) || 
                        HasConditionEffect(ConditionEffects.SamuraiBerserk))
                return false;
            
            return true;
        }

        internal void SetNewbiePeriod()
        {
            _newbieTime = 3000;
        }

        internal void SetTPDisabledPeriod()
        {
            _canTpCooldownTime = 10 * 1000; // 10 seconds
        }

        public bool IsVisibleToEnemy()
        {
            if (HasConditionEffect(ConditionEffects.Paused))
                return false;
            if (HasConditionEffect(ConditionEffects.Invisible))
                return false;
            if (HasConditionEffect(ConditionEffects.Hidden))
                return false;
            if (_newbieTime > 0)
                return false;
            return true;
        }

        public bool TPCooledDown()
        {
            if (_canTpCooldownTime > 0)
                return false;
            return true;
        }
    }
}
