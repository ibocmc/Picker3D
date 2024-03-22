using System;
using Cinemachine;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Extensions
{
    
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LockCinemachineAxis : CinemachineExtension
    {

        [SerializeField] private CinemachineLockAxis _lockAxis;
        
        [Tooltip("Lock the Cinamachine Virtual Camera's X Axis Position With this value.")]
        public float XclampValue = 0;
  
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {

            switch (_lockAxis)
            {
                case CinemachineLockAxis.X:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.x = XclampValue;
                        state.RawPosition = pos;
                    }
                    break;
                case CinemachineLockAxis.Y:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.y = XclampValue;
                        state.RawPosition = pos;
                    }
                    break;
                case CinemachineLockAxis.Z:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.z = XclampValue;
                        state.RawPosition = pos;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}