using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Itop.WinService {
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer {
        public ProjectInstaller() {
            InitializeComponent();
            this.serviceInstaller1.ServiceName = ItopService.GetServiceName();
        }
    }
}