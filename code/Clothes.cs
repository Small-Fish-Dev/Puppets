global using Sandbox;

namespace Puppets
{
	public struct Clothes
	{
		public string Skin { get; private set; }
		public Clothing SkinClothing => ResourceLibrary.Get<Clothing>( Skin );
		public string Head { get; private set; }
		public Clothing HeadClothing => ResourceLibrary.Get<Clothing>( Head );
		public string Face { get; private set; }
		public Clothing FaceClothing => ResourceLibrary.Get<Clothing>( Face );
		public string Top { get; private set; }
		public Clothing TopClothing => ResourceLibrary.Get<Clothing>( Top );
		public string Bottom { get; private set; }
		public Clothing BottomClothing => ResourceLibrary.Get<Clothing>( Bottom );
		public string Shoes { get; private set; }
		public Clothing ShoesClothing => ResourceLibrary.Get<Clothing>( Shoes );

		public Clothes() { }

		public Clothes( string skin, string head, string face, string top, string bottom, string shoes )
		{
			Skin = skin;
			Head = head;
			Face = face;
			Top = top;
			Bottom = bottom;
			Shoes = shoes;
		}

		public void Dress( AnimatedEntity target )
		{
			var container = new ClothingContainer();
			container.Toggle( SkinClothing );
			container.Toggle( HeadClothing );
			container.Toggle( FaceClothing );
			container.Toggle( TopClothing );
			container.Toggle( BottomClothing );
			container.Toggle( ShoesClothing );

			container.DressEntity( target );
		}

		public static Clothes[] Presets { get; private set; } = new Clothes[]
		{
			new Clothes( "models/citizen_clothes/skin01.clothing", "models/citizen_clothes/hair/hair_oldcurly/hair_oldcurly_grey.clothing", "models/citizen_clothes/hair/eyebrows_bushy/eyebrows_bushy.clothing", "models/citizen_clothes/shirt/army_shirt/army_shirt.clothing", "models/citizen_clothes/trousers/cargopants/cargo_pants_army.clothing", "models/citizen_clothes/shoes/boots/army_boots.clothing" ),

		};
	}
}
