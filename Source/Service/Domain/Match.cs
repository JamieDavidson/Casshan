﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Casshan.Domain
{
    internal sealed class Match
    {
        public IReadOnlyCollection<Account> HumanParticipants { get; }
        public IReadOnlyCollection<int> RuneIds { get; }
        public SummonerSpellPair SpellIds { get; }
        public VisionStats TargetVisionStats { get; }
        public int MinionsKilled { get; }


        public Match(IEnumerable<Account> humanParticipants,
                     IEnumerable<int> targetRuneIds,
                     SummonerSpellPair targetSpellIds,
                     VisionStats visionStats,
                     int minionsKilled)
        {
            if (humanParticipants == null)
            {
                throw new ArgumentNullException(nameof(humanParticipants));
            }

            RuneIds = targetRuneIds?.ToArray() ?? throw new ArgumentNullException(nameof(targetRuneIds));
            SpellIds = targetSpellIds;

            var humans = humanParticipants.ToArray();

            if (humans.Contains(null))
            {
                throw new ArgumentException("Must not contain null", nameof(humanParticipants));
            }

            HumanParticipants = humans;
            TargetVisionStats = visionStats;
            MinionsKilled = minionsKilled;
        }
    }
}
