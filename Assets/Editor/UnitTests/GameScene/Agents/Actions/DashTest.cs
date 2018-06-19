using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Reflection;

namespace GameScene.Agents.Actions.Test {

    public class DashTest {

        GameObject actionObj;

        Dash dash;
        Rigidbody2D rigidbody;
        IAgent agent;

        [SetUp]
        public void SetUp() {
            agent = Substitute.For<IAgent>();
            actionObj = new GameObject("dash", typeof(Dash));
            rigidbody = new GameObject("rigidbody", typeof(Rigidbody2D)).GetComponent<Rigidbody2D>();
            dash = actionObj.GetComponent<Dash>();
            agent.RigidbodyCache.Returns(rigidbody);

            dash.Speed = 100;
            dash.MinimumSpeed = 10;
            dash.ConstantSpeedFrames = 5;
            dash.DashFrameLimit = 10;

            PropertyInfo AgentProperty = dash.GetType().BaseType.GetProperty("Agent", BindingFlags.Instance | BindingFlags.NonPublic);
            AgentProperty.SetValue(dash, agent, null);
        }

        [Test]
        public void ダッシュ開始時に速度がSpeedの値になること() {
            float expected = dash.Speed;

            dash.GetType().InvokeMember("DoDash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, dash, new object[] { EightDirection.Right });

            Assert.AreEqual(expected, agent.RigidbodyCache.velocity.x);
        }

        [Test]
        public void DashFrameLimitフレーム後に速度がMinimumSpeedになること() {
            float expected = dash.MinimumSpeed;

            dash.GetType().InvokeMember("DoDash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, dash, new object[] { EightDirection.Right });

            for (int i = 0; i < dash.DashFrameLimit; ++i) {
                dash.GetType().InvokeMember("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, dash, null);
            }

            Assert.AreEqual(expected, agent.RigidbodyCache.velocity.x);
        }

        [Test]
        public void ConstantSpeedFramesのひとつ前のフレームで_速度がSpeedであること() {
            float expected = dash.Speed;

            dash.GetType().InvokeMember("DoDash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, dash, new object[] { EightDirection.Right });

            for (int i = 0; i < dash.ConstantSpeedFrames - 1; ++i) {
                dash.GetType().InvokeMember("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, dash, null);
            }

            Assert.AreEqual(expected, agent.RigidbodyCache.velocity.x);
        }

        [Test]
        public void DashFrameLimitの直前は_速度がMinimumSpeedよりも大きいこと() {
            float expected = dash.MinimumSpeed;

            dash.GetType().InvokeMember("DoDash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, dash, new object[] { EightDirection.Right });

            for (int i = 0; i < dash.DashFrameLimit - 1; ++i) {
                dash.GetType().InvokeMember("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, dash, null);
            }

            Assert.Greater(agent.RigidbodyCache.velocity.x, expected);
        }
    }

}