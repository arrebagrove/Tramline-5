﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TramlineFive.DataAccess.DomainLogic;

namespace TramlineFive.ViewModels.Wrappers
{
    public class DayViewModel
    {
        public DayViewModel(DayDO domain)
        {
            core = domain;
        }

        public string Type
        {
            get
            {
                return core.Type;
            }
        }

        public IEnumerable<StopViewModel> Stops
        {
            get
            {
                return core.Stops.Select(s => new StopViewModel(s));
            }
        }

        public async Task LoadStops()
        {
            await core.LoadStops();
        }

        private DayDO core;
    }
}
