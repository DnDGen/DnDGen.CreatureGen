﻿using DnDGen.CreatureGen.Creatures;
using System;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class HalfFiendApplicator : TemplateApplicator
    {
        public Creature ApplyTo(Creature creature)
        {
            throw new NotImplementedException();
        }

        public Task<Creature> ApplyToAsync(Creature creature)
        {
            throw new NotImplementedException();
        }

        public bool IsCompatible(string creature)
        {
            throw new NotImplementedException();
        }
    }
}
