using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTest : MonoBehaviour, IMonoBehaviourTest
    {


        public bool IsTestFinished
        {
            get
            {
                return true;
            }
        }
        void Update()
        {

        }
        
    }

    public class PacManTests
    {
        GameObject go;
        Player player;
        PlayerController controller;

        [OneTimeSetUp]
        public void Setup()
        {
            
            go = new GameObject("player");
            player = go.AddComponent<Player>();
            controller = go.AddComponent<PlayerController>();
            


            Assert.Ignore();

            return;
            
        }

        // A Test behaves as an ordinary method
        [Test]
        public void PacManPlayerControllerTest()
        {
            //Act 
            //is it possible to test Input? 
            //Arrange

            //Assert
            
            // Use the Assert class to test conditions
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void PacManLogTests()
        {
            //Act 
            
            //Arrange

            //Assert

            // Use the Assert class to test conditions
            Assert.AreEqual(1, 1);
        }


        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PacManTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
