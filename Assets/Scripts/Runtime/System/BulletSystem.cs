using Tower.Runtime.Gameplay;
using System.Collections.Generic;

namespace Tower.Runtime.GameSystem
{
    public class BulletSystem : SystemBase
    {   
        /// <summary>
        /// 所有存货的子弹
        /// </summary>
        public List<BulletBase> AllActiveBullet = new List<BulletBase>();

        /// <summary>
        /// 发射子弹
        /// </summary>
        /// <param name="bullet">待发射的子弹</param>
        /// <returns>发射的子弹</returns>
        public BulletBase FireBullet(BulletBase bullet)
        {
            AllActiveBullet.Add(bullet);

            return bullet;
        }

        /// <summary>
        /// 销毁子弹
        /// </summary>
        /// <param name="bullet">待销毁的子弹</param>
        /// <returns>销毁的子弹</returns>
        public BulletBase DestoryBullet(BulletBase bullet)
        {
            if (AllActiveBullet.Contains(bullet))
            {
                bullet.OnKill();

                return bullet;
            }

            return null;
        }

        /// <summary>
        /// 清除所有存活子弹
        /// </summary>
        public void DestoryAllBullet()
        {
            foreach (var bullet in AllActiveBullet)
            {
                bullet.OnKill();
            }

            AllActiveBullet.Clear();
        }
    }
}