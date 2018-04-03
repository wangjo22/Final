using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public static class Constant
    {
        public const int WINDOW_WIDTH = 672;
        public const int WINDOW_HEIGHT = 768;
        public const float ALIEN_SIDE_STEP = 10.0f;
        public const float ALIEN_WIDTH = 35.0f;
        public const float ALIEN_HEIGHT = 23.0f;
        public const float ALIEN_VERTICAL_STEP = ALIEN_HEIGHT / 2;
        public const float ALIEN_OFFSET_X = ALIEN_WIDTH + 10;
        public const float ALIEN_OFFSET_Y = ALIEN_HEIGHT + 10;
        public const int NUMBER_OF_ALIENS_PER_COLUMN = 5;
        public const float ALIEN_MOVEMENT_ANIMATION_DELTATIME = 0.3f;
        public const float PLAYER_SPEED = 2.0f;
        public const float SHIELD_BRICK_WIDTH = 15.0f;
        public const float SHIELD_BRICK_HEIGHT = 8.0f;
        public const float BOMB_FALL_SPEED = 2.0f;
    }
}
