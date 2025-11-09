using Tower.Runtime.Common;
using Tower.Runtime.Core;
using Tower.Runtime.Gameplay;

namespace Tower.Runtime.ToolKit
{
    public class GameKit
    {
        #region ==========Ê±¼äÖá==========
        public static Timeline PlayTimeline(TimelineModel model, object caster) => TimelineManager.Instance.PlayTimeline(model, caster);
        public static void StopTimeline(Timeline timeline) => TimelineManager.Instance.StopTimeline(timeline);
        public static void StopAllTimelines() => TimelineManager.Instance.StopAllTimelines();
        #endregion

        #region ==========AoE==========
        public static void CreateAoE(AoEBase aoe) => AoEManager.Instance.CreateAoE(aoe);
        public static void DestroyAoE(AoEBase aoe) => AoEManager.Instance.DestroyAoE(aoe);
        public static void ClearAllAoE() => AoEManager.Instance.ClearAllAoE();
        #endregion
    }
}