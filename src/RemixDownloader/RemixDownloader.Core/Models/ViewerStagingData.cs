using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class ViewerStagingData
    {
        [JsonProperty("Application.RotationSpeedModel")]
        public double ApplicationRotationSpeedModel { get; set; }

        [JsonProperty("Camera.Behaviour")]
        public double CameraBehaviour { get; set; }

        [JsonProperty("Camera.Contrast")]
        public double CameraContrast { get; set; }

        [JsonProperty("Camera.DefaultElevation")]
        public double CameraDefaultElevation { get; set; }

        [JsonProperty("Camera.ElevationLowerLimit (degrees)")]
        public double CameraElevationLowerLimitDegrees { get; set; }

        [JsonProperty("Camera.ElevationReturnTime (s)")]
        public double CameraElevationReturnTimeS { get; set; }

        [JsonProperty("Camera.ElevationReturnWaitTime (s)")]
        public double CameraElevationReturnWaitTimeS { get; set; }

        [JsonProperty("Camera.ElevationUpperLimit (degrees)")]
        public double CameraElevationUpperLimitDegrees { get; set; }

        [JsonProperty("Camera.ExposureValue")]
        public double CameraExposureValue { get; set; }

        [JsonProperty("Camera.FieldOfView (Degrees)")]
        public double CameraFieldOfViewDegrees { get; set; }

        [JsonProperty("Camera.FrameOnModelLoad")]
        public bool CameraFrameOnModelLoad { get; set; }

        [JsonProperty("Camera.FramingBehaviour")]
        public double CameraFramingBehaviour { get; set; }

        [JsonProperty("Camera.FramingElevation")]
        public double CameraFramingElevation { get; set; }

        [JsonProperty("Camera.FramingPositionY")]
        public double CameraFramingPositionY { get; set; }

        [JsonProperty("Camera.FramingRadius")]
        public double CameraFramingRadius { get; set; }

        [JsonProperty("Camera.FramingRotation")]
        public double CameraFramingRotation { get; set; }

        [JsonProperty("Camera.FramingTime")]
        public double CameraFramingTime { get; set; }

        [JsonProperty("Camera.IdleRotationSpeed (degrees/s)")]
        public double CameraIdleRotationSpeedDegreesS { get; set; }

        [JsonProperty("Camera.IdleRotationSpinupTime (s)")]
        public double CameraIdleRotationSpinupTimeS { get; set; }

        [JsonProperty("Camera.IdleRotationWaitTime (s)")]
        public double CameraIdleRotationWaitTimeS { get; set; }

        [JsonProperty("Camera.MaxDistance")]
        public double CameraMaxDistance { get; set; }

        [JsonProperty("Camera.MinDistance")]
        public double CameraMinDistance { get; set; }

        [JsonProperty("Camera.State.Position.X")]
        public double CameraStatePositionX { get; set; }

        [JsonProperty("Camera.State.Position.Y")]
        public double CameraStatePositionY { get; set; }

        [JsonProperty("Camera.State.Position.Z")]
        public double CameraStatePositionZ { get; set; }

        [JsonProperty("Camera.State.Rotation.W")]
        public double CameraStateRotationW { get; set; }

        [JsonProperty("Camera.State.Rotation.X")]
        public double CameraStateRotationX { get; set; }

        [JsonProperty("Camera.State.Rotation.Y")]
        public double CameraStateRotationY { get; set; }

        [JsonProperty("Camera.State.Rotation.Z")]
        public double CameraStateRotationZ { get; set; }

        [JsonProperty("Camera.ToneMappingEnabled")]
        public bool CameraToneMappingEnabled { get; set; }

        [JsonProperty("Camera.ZoomStopsAnimation")]
        public bool CameraZoomStopsAnimation { get; set; }

        [JsonProperty("ColorGrading.ColorFilterDensityGlobal")]
        public double ColorGradingColorFilterDensityGlobal { get; set; }

        [JsonProperty("ColorGrading.ColorFilterDensityHighlights")]
        public double ColorGradingColorFilterDensityHighlights { get; set; }

        [JsonProperty("ColorGrading.ColorFilterDensityMidtones")]
        public double ColorGradingColorFilterDensityMidtones { get; set; }

        [JsonProperty("ColorGrading.ColorFilterDensityShadows")]
        public double ColorGradingColorFilterDensityShadows { get; set; }

        [JsonProperty("ColorGrading.ColorFilterHueGlobal")]
        public double ColorGradingColorFilterHueGlobal { get; set; }

        [JsonProperty("ColorGrading.ColorFilterHueHighlights")]
        public double ColorGradingColorFilterHueHighlights { get; set; }

        [JsonProperty("ColorGrading.ColorFilterHueMidtones")]
        public double ColorGradingColorFilterHueMidtones { get; set; }

        [JsonProperty("ColorGrading.ColorFilterHueShadows")]
        public double ColorGradingColorFilterHueShadows { get; set; }

        [JsonProperty("ColorGrading.ExposureGlobal")]
        public double ColorGradingExposureGlobal { get; set; }

        [JsonProperty("ColorGrading.ExposureHighlights")]
        public double ColorGradingExposureHighlights { get; set; }

        [JsonProperty("ColorGrading.ExposureMidtones")]
        public double ColorGradingExposureMidtones { get; set; }

        [JsonProperty("ColorGrading.ExposureShadows")]
        public double ColorGradingExposureShadows { get; set; }

        [JsonProperty("ColorGrading.SaturationGlobal")]
        public double ColorGradingSaturationGlobal { get; set; }

        [JsonProperty("ColorGrading.SaturationHighlights")]
        public double ColorGradingSaturationHighlights { get; set; }

        [JsonProperty("ColorGrading.SaturationMidtones")]
        public double ColorGradingSaturationMidtones { get; set; }

        [JsonProperty("ColorGrading.SaturationShadows")]
        public double ColorGradingSaturationShadows { get; set; }

        [JsonProperty("ColorGrading.TransformData")]
        public string ColorGradingTransformData { get; set; }

        [JsonProperty("ColorGrading.TransformDataFormat")]
        public string ColorGradingTransformDataFormat { get; set; }

        [JsonProperty("ColorGrading.TransformWeight")]
        public double ColorGradingTransformWeight { get; set; }

        [JsonProperty("Config.Version")]
        public string ConfigVersion { get; set; }

        [JsonProperty("GroundPlane.BottomGridOpacity")]
        public double GroundPlaneBottomGridOpacity { get; set; }

        [JsonProperty("GroundPlane.BottomVisible")]
        public bool GroundPlaneBottomVisible { get; set; }

        [JsonProperty("ImageProcessing.BloomEnabled")]
        public bool ImageProcessingBloomEnabled { get; set; }

        [JsonProperty("ImageProcessing.BloomQuality")]
        public double ImageProcessingBloomQuality { get; set; }

        [JsonProperty("ImageProcessing.BloomWeight")]
        public double ImageProcessingBloomWeight { get; set; }

        [JsonProperty("ImageProcessing.Enabled")]
        public bool ImageProcessingEnabled { get; set; }

        [JsonProperty("ImageProcessing.PlanarReflectionBlur")]
        public bool ImageProcessingPlanarReflectionBlur { get; set; }

        [JsonProperty("ImageProcessing.PlanarReflectionDirect")]
        public bool ImageProcessingPlanarReflectionDirect { get; set; }

        [JsonProperty("ImageProcessing.PlanarReflectionEnabled")]
        public bool ImageProcessingPlanarReflectionEnabled { get; set; }

        [JsonProperty("ImageProcessing.PlanarReflectionQuality")]
        public double ImageProcessingPlanarReflectionQuality { get; set; }

        [JsonProperty("ImageProcessing.PlanarReflectionWeight")]
        public double ImageProcessingPlanarReflectionWeight { get; set; }

        [JsonProperty("ImageProcessing.VignetteCentreX")]
        public double ImageProcessingVignetteCentreX { get; set; }

        [JsonProperty("ImageProcessing.VignetteCentreY")]
        public double ImageProcessingVignetteCentreY { get; set; }

        [JsonProperty("ImageProcessing.VignetteStretch")]
        public double ImageProcessingVignetteStretch { get; set; }

        [JsonProperty("ImageProcessing.VignetteWeight")]
        public double ImageProcessingVignetteWeight { get; set; }

        [JsonProperty("Lighting.BackgroundColorAmount")]
        public double LightingBackgroundColorAmount { get; set; }

        [JsonProperty("Lighting.BackgroundColorB")]
        public double LightingBackgroundColorB { get; set; }

        [JsonProperty("Lighting.BackgroundColorG")]
        public double LightingBackgroundColorG { get; set; }

        [JsonProperty("Lighting.BackgroundColorR")]
        public double LightingBackgroundColorR { get; set; }

        [JsonProperty("Lighting.BackgroundShadowAmount")]
        public double LightingBackgroundShadowAmount { get; set; }

        [JsonProperty("Lighting.DirectEnabled")]
        public bool LightingDirectEnabled { get; set; }

        [JsonProperty("Lighting.DirectIntensity")]
        public double LightingDirectIntensity { get; set; }

        [JsonProperty("Lighting.Effect_EnableShadow")]
        public bool LightingEffectEnableShadow { get; set; }

        [JsonProperty("Lighting.Effect_ShadowFieldOfView (Degrees)")]
        public double LightingEffectShadowFieldOfViewDegrees { get; set; }

        [JsonProperty("Lighting.Effect_ShadowsFarClip")]
        public double LightingEffectShadowsFarClip { get; set; }

        [JsonProperty("Lighting.Effect_ShadowsNearClip")]
        public double LightingEffectShadowsNearClip { get; set; }

        [JsonProperty("Lighting.EmissiveIntensity")]
        public double LightingEmissiveIntensity { get; set; }

        [JsonProperty("Lighting.EnvironmentDirect")]
        public double LightingEnvironmentDirect { get; set; }

        [JsonProperty("Lighting.EnvironmentEnabled")]
        public bool LightingEnvironmentEnabled { get; set; }

        [JsonProperty("Lighting.EnvironmentIndex")]
        public double LightingEnvironmentIndex { get; set; }

        [JsonProperty("Lighting.EnvironmentIntensity")]
        public double LightingEnvironmentIntensity { get; set; }

        [JsonProperty("Lighting.EnvironmentRotation")]
        public double LightingEnvironmentRotation { get; set; }

        [JsonProperty("Lighting.Light0_CameraRelative")]
        public bool LightingLight0CameraRelative { get; set; }

        [JsonProperty("Lighting.Light0_ColorB")]
        public double LightingLight0ColorB { get; set; }

        [JsonProperty("Lighting.Light0_ColorG")]
        public double LightingLight0ColorG { get; set; }

        [JsonProperty("Lighting.Light0_ColorR")]
        public double LightingLight0ColorR { get; set; }

        [JsonProperty("Lighting.Light0_Enabled")]
        public bool LightingLight0Enabled { get; set; }

        [JsonProperty("Lighting.Light0_Intensity")]
        public double LightingLight0Intensity { get; set; }

        [JsonProperty("Lighting.Light0_IntensityMode")]
        public double LightingLight0IntensityMode { get; set; }

        [JsonProperty("Lighting.Light0_PositionX")]
        public double LightingLight0PositionX { get; set; }

        [JsonProperty("Lighting.Light0_PositionY")]
        public double LightingLight0PositionY { get; set; }

        [JsonProperty("Lighting.Light0_PositionZ")]
        public double LightingLight0PositionZ { get; set; }

        [JsonProperty("Lighting.Light0_Radius")]
        public double LightingLight0Radius { get; set; }

        [JsonProperty("Lighting.Light0_SpotAngle")]
        public double LightingLight0SpotAngle { get; set; }

        [JsonProperty("Lighting.Light0_Type")]
        public double LightingLight0Type { get; set; }

        [JsonProperty("Lighting.Light1_CameraRelative")]
        public bool LightingLight1CameraRelative { get; set; }

        [JsonProperty("Lighting.Light1_ColorB")]
        public double LightingLight1ColorB { get; set; }

        [JsonProperty("Lighting.Light1_ColorG")]
        public double LightingLight1ColorG { get; set; }

        [JsonProperty("Lighting.Light1_ColorR")]
        public double LightingLight1ColorR { get; set; }

        [JsonProperty("Lighting.Light1_Enabled")]
        public bool LightingLight1Enabled { get; set; }

        [JsonProperty("Lighting.Light1_Intensity")]
        public double LightingLight1Intensity { get; set; }

        [JsonProperty("Lighting.Light1_IntensityMode")]
        public double LightingLight1IntensityMode { get; set; }

        [JsonProperty("Lighting.Light1_PositionX")]
        public double LightingLight1PositionX { get; set; }

        [JsonProperty("Lighting.Light1_PositionY")]
        public double LightingLight1PositionY { get; set; }

        [JsonProperty("Lighting.Light1_PositionZ")]
        public double LightingLight1PositionZ { get; set; }

        [JsonProperty("Lighting.Light1_Radius")]
        public double LightingLight1Radius { get; set; }

        [JsonProperty("Lighting.Light1_SpotAngle")]
        public double LightingLight1SpotAngle { get; set; }

        [JsonProperty("Lighting.Light1_Type")]
        public double LightingLight1Type { get; set; }

        [JsonProperty("Lighting.Light2_CameraRelative")]
        public bool LightingLight2CameraRelative { get; set; }

        [JsonProperty("Lighting.Light2_ColorB")]
        public double LightingLight2ColorB { get; set; }

        [JsonProperty("Lighting.Light2_ColorG")]
        public double LightingLight2ColorG { get; set; }

        [JsonProperty("Lighting.Light2_ColorR")]
        public double LightingLight2ColorR { get; set; }

        [JsonProperty("Lighting.Light2_Enabled")]
        public bool LightingLight2Enabled { get; set; }

        [JsonProperty("Lighting.Light2_Intensity")]
        public double LightingLight2Intensity { get; set; }

        [JsonProperty("Lighting.Light2_IntensityMode")]
        public double LightingLight2IntensityMode { get; set; }

        [JsonProperty("Lighting.Light2_PositionX")]
        public double LightingLight2PositionX { get; set; }

        [JsonProperty("Lighting.Light2_PositionY")]
        public double LightingLight2PositionY { get; set; }

        [JsonProperty("Lighting.Light2_PositionZ")]
        public double LightingLight2PositionZ { get; set; }

        [JsonProperty("Lighting.Light2_Radius")]
        public double LightingLight2Radius { get; set; }

        [JsonProperty("Lighting.Light2_SpotAngle")]
        public double LightingLight2SpotAngle { get; set; }

        [JsonProperty("Lighting.Light2_Type")]
        public double LightingLight2Type { get; set; }

        [JsonProperty("Lighting.MasterEnabled")]
        public bool LightingMasterEnabled { get; set; }

        [JsonProperty("Model.PositionAlignBase")]
        public bool ModelPositionAlignBase { get; set; }

        [JsonProperty("Model.PositionOffsetX")]
        public double ModelPositionOffsetX { get; set; }

        [JsonProperty("Model.PositionOffsetY")]
        public double ModelPositionOffsetY { get; set; }

        [JsonProperty("Model.PositionOffsetZ")]
        public double ModelPositionOffsetZ { get; set; }

        [JsonProperty("Model.RelativeModelLoadScale")]
        public double ModelRelativeModelLoadScale { get; set; }

        [JsonProperty("Model.RotationOffsetAngle")]
        public double ModelRotationOffsetAngle { get; set; }

        [JsonProperty("Model.RotationOffsetAxisX")]
        public double ModelRotationOffsetAxisX { get; set; }

        [JsonProperty("Model.RotationOffsetAxisY")]
        public double ModelRotationOffsetAxisY { get; set; }

        [JsonProperty("Model.RotationOffsetAxisZ")]
        public double ModelRotationOffsetAxisZ { get; set; }

        [JsonProperty("Renderer.ClearColorA")]
        public double RendererClearColorA { get; set; }

        [JsonProperty("Renderer.ClearColorB")]
        public double RendererClearColorB { get; set; }

        [JsonProperty("Renderer.ClearColorG")]
        public double RendererClearColorG { get; set; }

        [JsonProperty("Renderer.ClearColorOverride")]
        public bool RendererClearColorOverride { get; set; }

        [JsonProperty("Renderer.ClearColorR")]
        public double RendererClearColorR { get; set; }
    }
}
