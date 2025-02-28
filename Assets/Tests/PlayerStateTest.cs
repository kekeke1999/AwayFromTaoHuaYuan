using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerStateTests
{
    private GameObject _gameObject;
    private PlayerState _playerState;
    private Animator _animator;

    [SetUp]
    public void Setup()
    {
        _gameObject = new GameObject("Player");
        _playerState = _gameObject.AddComponent<PlayerState>();
        _animator = _gameObject.AddComponent<Animator>();

        _playerState.setAnimator(_animator);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_gameObject);
    }

     // Test method marked with UnityTest attribute to test the process of player moving to the left lane.
    [UnityTest]
    public IEnumerator TransitionToLeftLane()
    {
        _gameObject.transform.position = new Vector3(435f, 0, 0);
        _playerState.setIsTransitioning(false);

        yield return _playerState.TransitionToLane(new Vector3(-14f, 0, 0));

        Assert.AreEqual(421f, _gameObject.transform.position.x, 0.1f);
        Assert.IsFalse(_playerState.getIsTransitioning());
    }

    // Test method to verify the behavior of setting Animator flags during a jump action.
    [UnityTest]
    public IEnumerator JumpSetsIsJumpAnimatorFlag()
    {
        yield return _playerState.Jump(() => { });
        Assert.IsFalse(_animator.GetBool("IsJump"));
    }

    // Test method to test the process of player moving to the right lane.
    [UnityTest]
    public IEnumerator TransitionToRightLane()
    {
        _gameObject.transform.position = new Vector3(445f, 0, 0);
        _playerState.setIsTransitioning(false);

        yield return _playerState.TransitionToLane(new Vector3(14f, 0, 0));

        Assert.AreEqual(459f, _gameObject.transform.position.x, 0.1f);
        Assert.IsFalse(_playerState.getIsTransitioning());
    }
}
