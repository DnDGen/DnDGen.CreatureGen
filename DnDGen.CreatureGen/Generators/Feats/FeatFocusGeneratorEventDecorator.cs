using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.EventGen;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Feats
{
    internal class FeatFocusGeneratorEventDecorator : IFeatFocusGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IFeatFocusGenerator innerGenerator;

        public FeatFocusGeneratorEventDecorator(IFeatFocusGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities)
        {
            LogOpeningEvent(feat);
            var focus = innerGenerator.GenerateAllowingFocusOfAllFrom(feat, focusType, skills, abilities);
            LogClosingEvent(feat, focus);

            return focus;
        }

        private void LogOpeningEvent(string feat)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating focus for {feat}");
        }

        private void LogClosingEvent(string feat, string focus)
        {
            if (string.IsNullOrEmpty(focus))
                eventQueue.Enqueue("CreatureGen", $"Generated no focus for {feat}");
            else
                eventQueue.Enqueue("CreatureGen", $"Generated {feat}: {focus}");
        }

        public string GenerateAllowingFocusOfAllFrom(
            string feat,
            string focusType,
            IEnumerable<Skill> skills,
            IEnumerable<RequiredFeatSelection> requiredFeats,
            IEnumerable<Feat> otherFeats,
            int casterLevel,
            Dictionary<string, Ability> abilities,
            IEnumerable<Attack> attacks)
        {
            LogOpeningEvent(feat);
            var focus = innerGenerator.GenerateAllowingFocusOfAllFrom(feat, focusType, skills, requiredFeats, otherFeats, casterLevel, abilities, attacks);
            LogClosingEvent(feat, focus);

            return focus;
        }

        public string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities)
        {
            LogOpeningEvent(feat);
            var focus = innerGenerator.GenerateFrom(feat, focusType, skills, abilities);
            LogClosingEvent(feat, focus);

            return focus;
        }

        public string GenerateFrom(
            string feat,
            string focusType,
            IEnumerable<Skill> skills,
            IEnumerable<RequiredFeatSelection> requiredFeats,
            IEnumerable<Feat> otherFeats,
            int casterLevel,
            Dictionary<string, Ability> abilities,
            IEnumerable<Attack> attacks)
        {
            LogOpeningEvent(feat);
            var focus = innerGenerator.GenerateFrom(feat, focusType, skills, requiredFeats, otherFeats, casterLevel, abilities, attacks);
            LogClosingEvent(feat, focus);

            return focus;
        }

        public bool FocusTypeIsPreset(string focusType)
        {
            return innerGenerator.FocusTypeIsPreset(focusType);
        }
    }
}
