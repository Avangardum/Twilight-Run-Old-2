using System.Collections.Generic;

namespace Avangardum.TwilightRun
{
    public interface ITutorialConfig
    {
        List<TutorialHintData> TutorialHintData { get; }
    }
}