using Caliburn.Micro;
using HRPMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIStrings = HRPMonitor.Properties.Resources;


namespace HRPMonitor.ViewModels
{
    public class PauseDialogViewModel : Screen
    {
        
        private readonly double firstPausePeriodTime = 1;
        private readonly double secondPausePeriodTime = 5;
        private readonly double thirdPausePeriodTime = 10;
        private readonly IEventAggregator _eventAggregator;

        public string PauseMenuFirstOption
        {
            get
            {
                return string.Format(UIStrings.PauseMenuFirstOption, firstPausePeriodTime);
            }
        }
        public string PauseMenuSecondOption
        {
            get
            {
                return string.Format(UIStrings.PauseMenuSecondOption, secondPausePeriodTime);
            }
        }
        public string PauseMenuThirdOption
        {
            get
            {
                return string.Format(UIStrings.PauseMenuThirdOption, thirdPausePeriodTime);
            }
        }

        public PauseDialogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void PauseForFirst()
        {
            PublishPauseEvent(firstPausePeriodTime);
        }
        public void PauseForSecond()
        {
            PublishPauseEvent(secondPausePeriodTime);
        }
        public void PauseForThird()
        {
            PublishPauseEvent(thirdPausePeriodTime);
        }

        private void PublishPauseEvent(double pausePeriodTime)
        {
            _eventAggregator.PublishOnUIThread(new PauseOptionModel { SelectedPausePeriodTime = pausePeriodTime });
        }
    }
}
