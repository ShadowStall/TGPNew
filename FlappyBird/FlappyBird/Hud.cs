using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using System.Collections.Generic;

namespace FlappyBird
{
	public class Hud
	{
		private SpriteUV sprite;
		private TextureInfo textureInfo;
		public Hud (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/HUD/BulletCounterSymbol.png");
			sprite = new SpriteUV();
			sprite = new SpriteUV(textureInfo);	
			sprite.Quad.S = textureInfo.TextureSizef;
			sprite.Scale = new Vector2(0.2f);
			sprite.Position =  new Vector2(0f, Director.Instance.GL.Context.GetViewport().Height-50f);
			scene.AddChild(sprite);
		}
	}
}

