using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using GameScene.Agents.Actions;
using GameScene.Agents;
using GameScene.Referers;
using GameScene;

public class DashTest {

    GameObject actionObj;

    Dash dash;
    AgentReferer agentReferer;
    Agent agent;

    [SetUp]
    public void SetUp() {
        agent = new GameObject("Agent", typeof(Agent)).GetComponent<Agent>();
        actionObj = new GameObject("dash", typeof(Dash), typeof(AgentReferer));
        dash = actionObj.GetComponent<Dash>();
        agentReferer = actionObj.GetComponent<AgentReferer>();
        agentReferer.agent = agent;

        dash.Speed = 100;
        dash.MinimumSpeed = 10;
        dash.ConstantSpeedFrames = 5;
        dash.DashFrameLimit = 10;

        agent.GetType().InvokeMember("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, agent, null);
        dash.GetType().InvokeMember("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, dash, null);

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
