using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Workflow.Activities;
using Composite.C1Console.Actions;
using Composite.Data;
using Composite.Data.DynamicTypes.Foundation;
using Composite.Data.ProcessControlled;
using Composite.C1Console.Events;
using Composite.Core.ResourceSystem;
using Composite.Core.Extensions;
using Composite.C1Console.Workflow;


namespace Composite.C1Console.Elements.ElementProviderHelpers.AssociatedDataElementProviderHelper
{
    [EntityTokenLock()]
    [AllowPersistingWorkflow(WorkflowPersistingType.Idle)]
    public sealed partial class DeleteAssociatedDataWorkflow : Composite.C1Console.Workflow.Activities.FormsWorkflow
    {
        public DeleteAssociatedDataWorkflow()
        {
            InitializeComponent();
        }


        private void HasDataReferences(object sender, ConditionalEventArgs e)
        {
            IData data = ((DataEntityToken)this.EntityToken).Data;

            var brokenReferences = new List<IData>();

            var references = DataReferenceFacade.GetNotOptionalReferences(data);
            foreach (var reference in references)
            {
                DataSourceId dataSourceId = reference.DataSourceId;
                if (brokenReferences.Any(brokenRef => brokenRef.DataSourceId == dataSourceId))
                {
                    continue;
                }

                brokenReferences.Add(reference);
            }

            e.Result = brokenReferences.Count > 0;
            if (brokenReferences.Count == 0)
            {
                return;
            }

            Bindings.Add("ReferencedData", DataReferenceFacade.GetBrokenReferencesReport(brokenReferences));
        }

        

        private void finalizeCodeActivity_ExecuteCode(object sender, EventArgs e)
        {
            DeleteTreeRefresher deleteTreeRefresher = this.CreateDeleteTreeRefresher(this.EntityToken);

            IData data = ((DataEntityToken)this.EntityToken).Data;

            if (DataFacade.WillDeleteSucceed(data))
            {
                ProcessControllerFacade.FullDelete(data);

                deleteTreeRefresher.PostRefreshMesseges(true);
            }
            else
            {
                this.ShowMessage(
                        DialogType.Error,
                        StringResourceSystemFacade.GetString("Composite.Management", "DeleteAssociatedDataWorkflow.CascadeDeleteErrorTitle"),
                        StringResourceSystemFacade.GetString("Composite.Management", "DeleteAssociatedDataWorkflow.CascadeDeleteErrorMessage")
                    );

            }
        }
    }
}
