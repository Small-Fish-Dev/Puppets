﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Puppets
{
	public struct Clothes
	{
		public string Skin { get; private set; } = "models/citizen_clothes/skin01.clothing";
		public Clothing SkinClothing => ResourceLibrary.Get<Clothing>( Skin );
		public string Head { get; private set; } = "models/citizen_clothes/hair/hair_oldcurly/hair_oldcurly_grey.clothing";
		public Clothing HeadClothing => ResourceLibrary.Get<Clothing>( Head );
		public string Face { get; private set; } = "models/citizen_clothes/hair/eyebrows_bushy/eyebrows_bushy.clothing";
		public Clothing FaceClothing => ResourceLibrary.Get<Clothing>( Face );
		public string Top { get; private set; } = "models/citizen_clothes/shirt/army_shirt/army_shirt.clothing";
		public Clothing TopClothing => ResourceLibrary.Get<Clothing>( Top );
		public string Bottom { get; private set; } = "models/citizen_clothes/trousers/cargopants/cargo_pants_army.clothing";
		public Clothing BottomClothing => ResourceLibrary.Get<Clothing>( Bottom );
		public string Shoes { get; private set; } = "models/citizen_clothes/shoes/boots/army_boots.clothing";
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
			new Clothes()
		};
	}

	public partial class Puppet : AnimatedEntity
	{
		[Net] public string Username { get; private set; } = "Anonymous";
		public NameTag NameTag { get; private set; }
		public bool IsTalking { get; private set; } = false;
		public int Identifier { get; private set; } = 0;
		public bool LooksAtYou { get; private set; } = false;
		public Vector3 Target => Game.Clients.FirstOrDefault().Pawn?.Position ?? Rotation.Forward;

		public Puppet() { }
		
		public Puppet( string username )
		{
			Username = username;
		}

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/citizen/citizen.vmdl" );

			Identifier = Entity.All.OfType<Puppet>().Count();
		}

		public override void ClientSpawn()
		{
			base.ClientSpawn();

			NameTag = new NameTag();
			NameTag.Position = Position + Vector3.Up * 76f;
		}

		public void Dress( int clothingId = 0 )
		{
			/*if ( clothingId == 0 )
				Clothes.Presets.Ran
			Clothes.Presets.*/
			new Clothes().Dress( this );
		}

		public void Dress( AnimatedEntity copy )
		{
			var clothingContainer = new ClothingContainer();

			foreach ( Entity child in copy.Children )
			{
				if ( child is not AnimatedEntity childModel ) return;

				Clothing clothing = new Clothing();
				clothing.Model = childModel.GetModelName();
				clothingContainer.Toggle( clothing );
			}

			clothingContainer.DressEntity( this );
		}

		[GameEvent.Tick.Server]
		public void ComputeActing()
		{
			if ( LooksAtYou )
			{
				var animationHelper = new CitizenAnimationHelper( this );
				animationHelper.WithLookAt( Target );
			}

			if ( Time.Tick % 4 == 0 )
				SetAnimParameter( "voice", IsTalking ? Game.Random.Float( 0, 1 ) : 0 );
		}

		[GameEvent.Client.Frame]
		public void MoveNameTag()
		{
			if ( NameTag != null )
			{
				if ( Username == "none" )
				{
					NameTag.Delete();
					return;
				}

				NameTag.Position = Position + Vector3.Up * 85f;
				NameTag.Rotation = Rotation.LookAt( Camera.Main.Position - NameTag.Position );
				NameTag.Label.Text = Username;
				var textSize = Username.Length * 150f;
				NameTag.PanelBounds = new Rect( 0, 0, textSize, 200 );
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			NameTag?.Delete();
		}

		[ConCmd.Server( "spawn_puppet" )]
		public static void SpawnPuppet( string username = "Anonymous", bool looksAtYou = true, int clothing = 0 )
		{
			if ( ConsoleSystem.Caller.Pawn is not AnimatedEntity player ) return;

			var spawnedPuppet = new Puppet();
			spawnedPuppet.Position = player.Position;
			spawnedPuppet.Rotation = player.Rotation;
			spawnedPuppet.Username = username;
			spawnedPuppet.LooksAtYou = looksAtYou;
		}

		[ConCmd.Server( "spawn_clone" )]
		public static void SpawnClone( string username = "default", bool looksAtYou = true )
		{
			if ( ConsoleSystem.Caller.Pawn is not AnimatedEntity player ) return;

			var spawnedPuppet = new Puppet();
			spawnedPuppet.Position = player.Position;
			spawnedPuppet.Rotation = player.Rotation;
			spawnedPuppet.Username = username == "default" ? player.Client.Name : username;
			spawnedPuppet.LooksAtYou = looksAtYou;

			spawnedPuppet.Dress( player );
		}

		[ConCmd.Server( "toggle_speech" )]
		public static void ToggleSpeech( int targetId = 0 )
		{
			var puppet = Entity.All.OfType<Puppet>().Where( x => x.Identifier == targetId ).FirstOrDefault();

			if ( puppet != null )
				puppet.IsTalking = !puppet.IsTalking;
		}

		[ConCmd.Server( "undo_puppet" )]
		public static void UndoPuppet()
		{
			var target = Entity.All.OfType<Puppet>().OrderBy( x => x.Identifier ).LastOrDefault();
			if ( target != null )
				target.Delete();
		}
	}

	public class NameTag : WorldPanel
	{
		public Label Label { get; set; }

		public NameTag()
		{
			StyleSheet.Load( "Style.scss" );
			Label = Add.Label( "Anonymous", "NameTag" );
		}
	}
}
