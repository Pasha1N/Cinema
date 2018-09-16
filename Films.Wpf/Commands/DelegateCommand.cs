﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Wpf.Commands
{
    public class DelegateCommand : Command
    {
        private Func<bool> canExecuteMethod;
        private Action executeMethod;

        public DelegateCommand(Action ExecuteMethod) :
            this(ExecuteMethod, () => true)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public override bool CanExecute()
        {
            return canExecuteMethod();
        }

        public override void Execute()
        {
            executeMethod();
        }
    }
}