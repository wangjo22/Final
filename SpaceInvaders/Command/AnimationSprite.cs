using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimationSprite : Command
    {
        public AnimationSprite(GameSprite.Name spriteName)
        {
            // initialized the sprite animation is attached to
            this.pSprite = GameSpriteManager.Find(spriteName);
            Debug.Assert(this.pSprite != null);

            // initialize references
            this.pCurrImage = null;

            // list
            this.poFirstImage = null;
        }

        public void Attach(Image.Name imageName)
        {
            // Get the image we are looking for from ImageManager
            Image pImage = ImageManager.Find(imageName);
            Debug.Assert(pImage != null);

            // Create a new holder
            ImageHolder pImageHolder = new ImageHolder(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite to the front of the image holder list.
            SLink.AddToFront(ref this.poFirstImage, pImageHolder);

            // Set the first one to this image
            this.pCurrImage = pImageHolder;
        }

        public override void Execute(float deltaTime)
        {
            // Get next image
            ImageHolder pImageHolder;
            if (this.pCurrImage.pNext != null)
            {
                pImageHolder = (ImageHolder)this.pCurrImage.pNext;
            }
            else
            {
                pImageHolder = (ImageHolder)poFirstImage;
            }

            // squirrel away for next timer event
            this.pCurrImage = pImageHolder;

            // change image
            this.pSprite.SwapImage(pImageHolder.pImage);

            // Add itself back to timer
            TimerManager.Add(TimeEvent.Name.AnimationSprite, this, TimerManager.GetAlienDeltatTime());
        }


        public void Dump()
        {
            ImageHolder pHolder = (ImageHolder)this.poFirstImage;

            while (pHolder != null)
            {
                Debug.WriteLine("Image: {0}", pHolder.pImage.GetName());
                pHolder = (ImageHolder)pHolder.pNext;
            }
        }
        // Data: ---------------
        private GameSprite pSprite;
        private SLink pCurrImage;
        private SLink poFirstImage;
    }
}
