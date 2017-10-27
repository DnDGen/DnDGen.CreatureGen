using CreatureGen.Leaders;

namespace CreatureGen.Domain.Selectors.Collections
{
    internal interface ILeadershipSelector
    {
        int SelectCohortLevelFor(int leadershipScore);
        FollowerQuantities SelectFollowerQuantitiesFor(int leadershipScore);
    }
}
