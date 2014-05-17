using System.Xml.Serialization;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ObstacleModel
	{
		#region Properties
		[XmlAttribute]
		public virtual string Name	{ get; set; }

		[XmlAttribute]
		public virtual double X		{ get; set; }

		[XmlAttribute]
		public virtual double Y		{ get; set; }
		#endregion


		#region Constructors
		public ObstacleModel() { }
		#endregion
	}
}
