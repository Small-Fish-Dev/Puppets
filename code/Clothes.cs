global using Sandbox;
using System.Collections.Generic;

namespace Puppets
{
	public struct Clothes
	{
		public List<string> Clothing { get; private set; } = new();

		public Clothes() { }

		public Clothes( List<string> clothing )
		{
			Clothing = clothing;
		}

		public void Dress( AnimatedEntity target )
		{
			var container = new ClothingContainer();

			foreach ( var piece in  Clothing )
			{
				var resource = ResourceLibrary.Get<Clothing>( piece );

				if ( resource != null )
					container.Toggle( resource );
			}

			container.DressEntity( target );
		}

		// Ugh I know I could do them like I did in Hamsteria but this isn't even a side project, perhaps if people want it in the future.
		public static Clothes[] Presets { get; private set; } = new Clothes[]
		{
			new Clothes( new List<string>()
			{ 
				"models/citizen_clothes/skin01.clothing",
				"models/citizen_clothes/hair/hair_oldcurly/hair_oldcurly_grey.clothing",
				"models/citizen_clothes/hair/eyebrows_bushy/eyebrows_bushy.clothing",
				"models/citizen_clothes/shirt/army_shirt/army_shirt.clothing",
				"models/citizen_clothes/trousers/cargopants/cargo_pants_army.clothing",
				"models/citizen_clothes/shoes/boots/army_boots.clothing" 
			} ),
			new Clothes( new List<string>()
			{ 
				"models/citizen_clothes/skin02.clothing",
				"models/citizen_clothes/hair/hair_balding/hair_baldinggrey.clothing",
				"models/citizen_clothes/hair/eyebrows_bushy/eyebrows_bushy.clothing",
				"models/citizen_clothes/shirt/hawaiian_shirt/hawaiian shirt.clothing",
				"models/citizen_clothes/shorts/summer_shorts/summer shorts.clothing",
				"models/citizen_clothes/shoes/slippers/slippers.clothing" 
			} ),
			new Clothes( new List<string>()
			{ 
				"models/citizen_clothes/skin03.clothing",
				"models/citizen_clothes/hat/fedora/fedora.clothing",
				"models/citizen_clothes/hair/eyebrows/eyebrows_black.clothing",
				"models/citizen_clothes/shirt/flannel_shirt/flannel_shirt.clothing",
				"models/citizen_clothes/trousers/jeans/jeans.clothing",
				"models/citizen_clothes/shoes/sneakers/sneakers.clothing"
			} ),
			new Clothes( new List<string>()
			{ 
				"models/citizen_clothes/skin04.clothing",
				"models/citizen_clothes/hair/hair_slickedback/hair_slickedback.clothing",
				"models/citizen_clothes/hair/eyebrows/eyebrows_black.clothing",
				"models/citizen_clothes/shirt/polo_shirt/polo_shirt.clothing",
				"models/citizen_clothes/trousers/trackiebottoms/trackie_bottoms.clothing",
				"models/citizen_clothes/shoes/crocs_and_socks/clogs_and_socks.clothing" 
			} ),
			new Clothes( new List<string>()
			{ 
				"models/citizen_clothes/skin01.clothing",
				"models/citizen_clothes/hair/big_scruffy_bread/big_scruffy_beard.clothing",
				"models/citizen_clothes/hair/eyebrows/eyebrows_black.clothing",
				"models/citizen_clothes/shirt/buttoned_shirt/buttoned_shirt.clothing",
				"models/citizen_clothes/shorts/shorts/shorts.clothing",
				"models/citizen_clothes/shoes/smartshoes/smartshoes.clothing" 
			} ),
			new Clothes( new List<string>()
			{ 
				"models/citizen_clothes/skin02.clothing",
				"models/citizen_clothes/hat/balaclava/balaclava.clothing",
				"models/citizen_clothes/hair/eyebrows/eyebrows.clothing",
				"models/citizen_clothes/shirt/tanktop/tanktop.clothing",
				"models/citizen_clothes/trousers/jeans/jeans.clothing",
				"models/citizen_clothes/shoes/boots/black_boots.clothing" 
			} ),
			new Clothes( new List<string>()
			{
				"models/citizen_clothes/skin03.clothing",
				"models/citizen_clothes/hair/hair_fade/hair_fade.clothing",
				"models/citizen_clothes/hair/eyebrows/eyebrows.clothing",
				"models/citizen_clothes/shirt/waistcoat_and_shirt/waistcoat_and_shirt.clothing",
				"models/citizen_clothes/trousers/smarttrousers/trousers.smart.clothing",
				"models/citizen_clothes/shoes/sneakers/blue_sneakers.clothing" 
			} ),
			new Clothes( new List<string>()
			{ 
				"models/citizen_clothes/skin04.clothing",
				"models/citizen_clothes/hair/hair_dreaded/hair_dreaded.clothing",
				"models/citizen_clothes/hair/moustache/moustache_brown.clothing",
				"models/citizen_clothes/jacket/hoodie/hoodie_black.clothing",
				"models/citizen_clothes/trousers/loosetrousers/loose_trousers.clothing",
				"models/citizen_clothes/shoes/smartshoes/smartshoes.clothing" 
			} ),
			new Clothes( new List<string>()
			{ 
				"models/citizen_clothes/skin01.clothing",
				"models/citizen_clothes/hair/hair_longbrown/models/hair_longgrey.clothing",
				"models/citizen_clothes/hair/eyebrows_bushy/eyebrows_bushy.clothing",
				"models/citizen_clothes/jumper/sleeveless_jumper/sleeveless_jumper_red.clothing",
				"models/citizen_clothes/shirt/buttoned_shirt/buttoned_shirt.clothing",
				"models/citizen_clothes/trousers/loosetrousers/loose_trousers.clothing",
				"models/citizen_clothes/hair/scruffy_beard/scruffy_beard_grey.clothing" ,
				"models/citizen_clothes/shoes/smartshoes/smartshoes.clothing"
			} ),

		};
	}
}
