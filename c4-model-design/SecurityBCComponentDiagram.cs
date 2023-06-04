using Structurizr;

namespace c4_model_design
{
	public class SecurityBCComponentDiagram
    {
		private readonly C4 c4;
		private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";

        public Component DomainLayer { get; private set; }
        public Component InterfaceLayer { get; private set; }
		public Component ApplicationLayer { get; private set; }
        public Component InfrastructureLayer { get; private set; }

		public SecurityBCComponentDiagram(C4 c4, ContainerDiagram containerDiagram)
		{
			this.c4 = c4;
			this.containerDiagram = containerDiagram;
		}

		public void Generate() {
			AddComponents();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddComponents()
		{
			DomainLayer = containerDiagram.SecurityBC.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            InterfaceLayer = containerDiagram.SecurityBC.AddComponent("Interface Layer", "", "NodeJS (NestJS)");
            ApplicationLayer = containerDiagram.SecurityBC.AddComponent("Application Layer", "", "NodeJS (NestJS)");
            InfrastructureLayer = containerDiagram.SecurityBC.AddComponent("Infrastructure Layer", "", "NodeJS (NestJS)");
		}

		private void AddRelationships() {
			containerDiagram.MobileApplication.Uses(InterfaceLayer, "", "JSON/HTTPS");
            containerDiagram.WebApplication.Uses(InterfaceLayer, "", "JSON/HTTPS");

            InterfaceLayer.Uses(ApplicationLayer, "", "");
            ApplicationLayer.Uses(DomainLayer, "", "");
			ApplicationLayer.Uses(InfrastructureLayer, "", "");
            InfrastructureLayer.Uses(DomainLayer, "", "");
			InfrastructureLayer.Uses(containerDiagram.Database, "Usa", "");
        }

        private void ApplyStyles() {
			SetTags();
		}

		private void SetTags()
		{
			DomainLayer.AddTags(this.componentTag);
            InterfaceLayer.AddTags(this.componentTag);
            ApplicationLayer.AddTags(this.componentTag);
            InfrastructureLayer.AddTags(this.componentTag);
		}

		private void CreateView() {
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.SecurityBC, "SecurityBC Component Diagram", "SecurityBC Component Diagram");
			componentView.Add(containerDiagram.MobileApplication);
			componentView.Add(containerDiagram.WebApplication);
			componentView.Add(containerDiagram.Database);
			componentView.AddAllComponents();
		}
	}
}