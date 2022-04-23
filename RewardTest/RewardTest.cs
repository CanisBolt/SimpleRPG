using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SimpleRPG;

namespace RewardTest
{
    [TestClass]
    public class RewardTest
    {
        [TestMethod]
        public void HeroAndEnemyHasSameLevel()
        {
            var actual = GameWindow.CheckLevelDifference(10, 10, 0);
            int rewardEXP = actual.Item1;
            int rewardGold = actual.Item2;
            Assert.AreEqual(rewardEXP, 10);
            Assert.AreEqual(rewardGold, 10);
        }

        [TestMethod]
        public void LevelDifferencePlus1()
        {
            var actual = GameWindow.CheckLevelDifference(10, 10, 1);

            int rewardEXP = actual.Item1;
            int rewardGold = actual.Item2;
            Assert.AreEqual(rewardEXP, 5);
            Assert.AreEqual(rewardGold, 5);
        }

        [TestMethod]
        public void LevelDifferenceMinus1()
        {
            var actual = GameWindow.CheckLevelDifference(10, 10, -1);

            int rewardEXP = actual.Item1;
            int rewardGold = actual.Item2;
            Assert.AreEqual(rewardEXP, 15);
            Assert.AreEqual(rewardGold, 15);
        }

        [TestMethod]
        public void EnemyRewardNoEXP()
        {
            var actual = GameWindow.CheckLevelDifference(10, 10, 10);

            int rewardEXP = actual.Item1;
            Assert.AreEqual(rewardEXP, 0);
        }

        [TestMethod]
        public void EnemyRewardOneGold()
        {
            var actual = GameWindow.CheckLevelDifference(0, -1, 0);

            int rewardGold = actual.Item2;
            Assert.AreEqual(rewardGold, 1);
        }

        [TestMethod]
        public void EnemyRewardNoGold()
        {
            var actual = GameWindow.CheckLevelDifference(0, 0, 0);

            int rewardGold = actual.Item2;
            Assert.AreEqual(rewardGold, 0);
        }
    }
}
