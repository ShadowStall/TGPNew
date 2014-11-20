using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace FlappyBird
{
	public class SpriteSheet : Scene
	{
		private SpriteTile _currentSprite;
		private Texture2D _texture;
		private TextureInfo _ti;
		private float elapsedTime = 0.0f;
		
		public SpriteSheet()
		{
			_texture = new Texture2D("/Application/textures/ship sheet.png",false);
			_ti = new TextureInfo(_texture,new Vector2i(4,2));
			_currentSprite = new SpriteTile(_ti,new Vector2i(0,1));
			_currentSprite.Pivot = new Vector2(0.5f,0.5f);
			_currentSprite.Position = new Vector2(0.0f,0.0f);
			_currentSprite.Scale = new Vector2(3.0f,3.0f);
			this.AddChild(_currentSprite);
			Scheduler.Instance.ScheduleUpdateForTarget(this,1,false);
		}
		public override void Update (float dt)
		{
			elapsedTime += dt;
			if(elapsedTime > 0.5f)
				{
					Vector2i currentTile = _currentSprite.TileIndex2D;
					if(currentTile.Y == 1)
					{
						if(currentTile.X < 3)
							currentTile.X ++;
						else
							currentTile = new Vector2i(0,0);
					}
					else
					{
						if(currentTile.X < 2)
							currentTile.X++;
						else
							currentTile = new Vector2i(0,1);
					}
					_currentSprite.TileIndex2D = currentTile;
					elapsedTime = 0.0f;
				}
		}

	}
}