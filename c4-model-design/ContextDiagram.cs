using Structurizr;

namespace c4_model_design
{
	public class ContextDiagram
	{
		private readonly C4 c4;
		public SoftwareSystem WorkFlowPro { get; private set; } //WorkFlowPro
		public SoftwareSystem GoogleCalendar { get; private set; } //Google Calendar: cronogramas
		public SoftwareSystem GoogleMeet { get; private set; } //Google Meet: reuniones
		public SoftwareSystem GoogleDrive {get; private set; } //Google Drive: guardar proyectos
		public SoftwareSystem EmailSystem {get; private set; }//Email system
		public SoftwareSystem Stripe {get; private set;} //Stripe system
		public SoftwareSystem Sunat {get; private set; }//Sunat: RUC de la empresa para verificar
		public SoftwareSystem Reniec {get; private set; }//Reniec: Verificar datos
		public Person Empleado { get; private set; } //Empleado
		public Person Jefe { get; private set; } //Jefe
		

		public ContextDiagram(C4 c4)
		{
			this.c4 = c4;
		}

		public void Generate() {
			AddElements();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddElements() {
			AddPeople();
			AddSoftwareSystems();
		}

		private void AddPeople()
		{
			Empleado = c4.Model.AddPerson("Empleado", "Empleado de la empresa que desea mejorar su comunicacion con otros miembros.");
			Jefe = c4.Model.AddPerson("Jefe", "Jefe de la empresa que supervisa y coordina los proyectos de los empleados.");
		}

		private void AddSoftwareSystems()
		{
			WorkFlowPro = c4.Model.AddSoftwareSystem("WorkFlowPro", "Software que permite la gestion de proyectos en las empresas.");
			GoogleCalendar = c4.Model.AddSoftwareSystem("Google Calendar", "Plataforma que ofrece una REST API de la informacion de un calendario.");
			GoogleMeet = c4.Model.AddSoftwareSystem("Google Meet", "Plataforma que ofrece una REST API de la informacion de una reunion.");
			GoogleDrive = c4.Model.AddSoftwareSystem("Google Drive","Plataforma que ofrece almacenamiento en la nube.");
			EmailSystem = c4.Model.AddSoftwareSystem("Email System ", "Plataforma que ofrece un servicio de correo electronico.");
			Stripe = c4.Model.AddSoftwareSystem("Stripe", "Plataforma que ofrece un servicio de pagos de manera segura y confiable");
			Sunat = c4.Model.AddSoftwareSystem("SUNAT ", "Plataforma que ofrece un servicio de verificacion de RUC.");
			Reniec = c4.Model.AddSoftwareSystem("RENIEC", "Plataforma que ofrece un servicio de verificacion de datos.");
		}

		private void AddRelationships() {
			Empleado.Uses(WorkFlowPro, "Realiza consultas para mantenerse al tanto de la planificación de los vuelos hasta la llegada del lote de vacunas al Perú");
			Jefe.Uses(WorkFlowPro, "Realiza consultas para mantenerse al tanto de la planificación de los vuelos hasta la llegada del lote de vacunas al Perú");

			WorkFlowPro.Uses(GoogleMeet, "Usa la API de Google Meet para realizar las reuniones de coordinación");
			WorkFlowPro.Uses(GoogleCalendar, "Usa la API de Google Calendar para agendar las reuniones de coordinación");
			WorkFlowPro.Uses(GoogleDrive, "Usa la API de Google Drive para guardar los proyectos");
			WorkFlowPro.Uses(EmailSystem, "Usa la API de Email System para enviar correos electronicos");
			WorkFlowPro.Uses(Stripe, "Usa la API de Stripe para los pagos de las empresas por nuestro servicio");
			WorkFlowPro.Uses(Sunat, "Usa la API de Sunat para verificar el RUC de la empresa");
			WorkFlowPro.Uses(Reniec, "Usa la API de Reniec para verificar los datos del usuario");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;
			
			styles.Add(new ElementStyle(nameof(Empleado)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
			styles.Add(new ElementStyle(nameof(Jefe)) { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });

			styles.Add(new ElementStyle(nameof(WorkFlowPro)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(GoogleCalendar)) { Background = "#d10202", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(GoogleMeet)) { Background = "#d10202", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(EmailSystem)){Background = "#d10202", Color = "#ffffff", Shape = Shape.RoundedBox});
			styles.Add(new ElementStyle(nameof(GoogleDrive)){Background = "#d10202", Color = "#ffffff", Shape = Shape.RoundedBox});
			styles.Add(new ElementStyle(nameof(Stripe)){Background = "#d10202", Color = "#ffffff", Shape = Shape.RoundedBox});
			styles.Add(new ElementStyle(nameof(Sunat)){Background = "#d10202", Color = "#ffffff", Shape = Shape.RoundedBox});
			styles.Add(new ElementStyle(nameof(Reniec)){Background = "#d10202", Color = "#ffffff", Shape = Shape.RoundedBox});
		}

		private void SetTags()
		{
			Empleado.AddTags(nameof(Empleado));
			Jefe.AddTags(nameof(Jefe));

			WorkFlowPro.AddTags(nameof(WorkFlowPro));
			GoogleCalendar.AddTags(nameof(GoogleCalendar));
			GoogleMeet.AddTags(nameof(GoogleMeet));
			GoogleDrive.AddTags(nameof(GoogleDrive));
			EmailSystem.AddTags(nameof(EmailSystem));
			Stripe.AddTags(nameof(Stripe));
			Sunat.AddTags(nameof(Sunat));
			Reniec.AddTags(nameof(Reniec));
		}

		private void CreateView() {
			SystemContextView contextView = c4.ViewSet.CreateSystemContextView(WorkFlowPro, "Contexto", "Diagrama de Contexto");
			contextView.AddAllSoftwareSystems();
			contextView.AddAllPeople();
		}
	}
}