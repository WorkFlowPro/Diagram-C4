using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
	public class C4
	{
        private readonly long workspaceId = 83196;
		private readonly string apiKey = "5c9c112c-11e9-41d5-a2db-f3c6c29397c3";
		private readonly string apiSecret = "651ba48c-2e65-4ea3-9227-eb06c45e08ba";

		public StructurizrClient StructurizrClient { get; }
		public Workspace Workspace { get; }
		public Model Model { get; }
		public ViewSet ViewSet { get; }

		public C4()
		{
			string workspaceName = "WorkFlowPro Diagrama 4C";
			string workspaceDescription = "Sistema de Monitoreo del Traslado Aéreo de Vacunas SARS-CoV-2";
			StructurizrClient = new StructurizrClient(apiKey, apiSecret);
			Workspace = new Workspace(workspaceName, workspaceDescription);
			Model = Workspace.Model;
			ViewSet = Workspace.Views;
		}

		public void Generate() {
			ContextDiagram contextDiagram = new ContextDiagram(this);
			ContainerDiagram containerDiagram = new ContainerDiagram(this, contextDiagram);
            WorkFlowProComponent monitoringComponentDiagram = new WorkFlowProComponent(this, contextDiagram, containerDiagram);
            AcountsBCComponentDiagram securityComponentDiagram = new AcountsBCComponentDiagram(this, containerDiagram);
            ResponsibilityBCComponentDiagram flightPlanningComponentDiagram = new ResponsibilityBCComponentDiagram(this, containerDiagram);
            PaymentsBCComponentDiagram airportComponentDiagram = new PaymentsBCComponentDiagram(this, containerDiagram);
            GroupBCComponentDiagram aircraftInventoryComponentDiagram = new GroupBCComponentDiagram(this, containerDiagram);
            SecurityBCComponentDiagram vaccinesInventoryComponentDiagram = new SecurityBCComponentDiagram(this, containerDiagram);
            contextDiagram.Generate();
			containerDiagram.Generate();
            monitoringComponentDiagram.Generate();
			securityComponentDiagram.Generate();
            flightPlanningComponentDiagram.Generate();
			airportComponentDiagram.Generate();
            aircraftInventoryComponentDiagram.Generate();
            vaccinesInventoryComponentDiagram.Generate();
            StructurizrClient.PutWorkspace(workspaceId, Workspace);
		}
	}
}