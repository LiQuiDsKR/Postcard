
//#if !UNITY_2020_1_OR_NEWER //Not needed anymore in 2020 and above
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Supyrb
{
	/// <summary>
	/// removes a warning popup for mobile builds, that this platform might not be supported:
	/// "Please note that Unity WebGL is not currently supported on mobiles. Press OK if you wish to continue anyway."
	/// </summary>
	public class RemoveMobileSupportWarningWebBuild
	{
		[PostProcessBuild]
		public static void OnPostProcessBuild(BuildTarget target, string targetPath)
		{
			if (target != BuildTarget.WebGL)
			{
				return;
			}

			var buildFolderPath = Path.Combine(targetPath, "Build");
			var info = new DirectoryInfo(buildFolderPath);
			var files = info.GetFiles("*.js");
			for (int i = 0; i < files.Length; i++)
			{
				var file = files[i];
				var filePath = file.FullName;
				var text = File.ReadAllText(filePath);
				text = text.Replace("UnityLoader.SystemInfo.mobile", "false");

				Debug.Log("Removing mobile warning from " + filePath);
				File.WriteAllText(filePath, text);
			}

			// index.html 파일 경로
			string indexPath = Path.Combine(buildFolderPath, "index.html");

			// Google Analytics 파일 경로
			string analyticsPath = Path.Combine(Application.dataPath, "Resources/Texts/GoogleAnalytics.txt");

			// 파일 읽기
			string htmlContent = File.ReadAllText(indexPath);
			string analyticsContent = File.ReadAllText(analyticsPath);

			// 타이틀 변경
			htmlContent = htmlContent.Replace("Unity WebGL Player | Postcard", "자분닷컴 | 엽서교환");

			// <head> 태그 뒤에 내용 추가
      		int headIndex = htmlContent.IndexOf("<head>") + "<head>".Length;
      		htmlContent = htmlContent.Insert(headIndex, System.Environment.NewLine + analyticsContent + System.Environment.NewLine);

			// 파일 쓰기
			File.WriteAllText(indexPath, htmlContent);
		}
	}
}
//#endif
