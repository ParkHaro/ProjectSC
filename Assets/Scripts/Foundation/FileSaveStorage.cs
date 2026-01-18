using System;
using System.IO;
using UnityEngine;

namespace Sc.Foundation
{
    /// <summary>
    /// 파일 기반 저장소 구현.
    /// Application.persistentDataPath에 JSON 파일로 저장.
    /// </summary>
    public class FileSaveStorage : ISaveStorage
    {
        private readonly string _basePath;

        /// <summary>
        /// 기본 생성자 (persistentDataPath 사용)
        /// </summary>
        public FileSaveStorage() : this(Application.persistentDataPath)
        {
        }

        /// <summary>
        /// 경로 지정 생성자 (테스트용)
        /// </summary>
        public FileSaveStorage(string basePath)
        {
            _basePath = basePath;
        }

        private string GetFilePath(string key) => Path.Combine(_basePath, $"{key}.json");

        public Result<bool> Save(string key, string data)
        {
            if (string.IsNullOrEmpty(key))
            {
                return Result<bool>.Failure(ErrorCode.SaveFailed, "저장 키가 비어있습니다.");
            }

            try
            {
                var filePath = GetFilePath(key);
                var directory = Path.GetDirectoryName(filePath);

                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(filePath, data);
                Log.Debug($"[FileSaveStorage] 저장 완료: {key}", LogCategory.Data);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                Log.Error($"[FileSaveStorage] 저장 실패: {key} - {e.Message}", LogCategory.Data);
                return Result<bool>.Failure(ErrorCode.SaveFailed, e.Message);
            }
        }

        public Result<string> Load(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return Result<string>.Failure(ErrorCode.LoadFailed, "로드 키가 비어있습니다.");
            }

            try
            {
                var filePath = GetFilePath(key);

                if (!File.Exists(filePath))
                {
                    return Result<string>.Failure(ErrorCode.LoadFailed, $"파일이 존재하지 않습니다: {key}");
                }

                var data = File.ReadAllText(filePath);
                Log.Debug($"[FileSaveStorage] 로드 완료: {key}", LogCategory.Data);
                return Result<string>.Success(data);
            }
            catch (Exception e)
            {
                Log.Error($"[FileSaveStorage] 로드 실패: {key} - {e.Message}", LogCategory.Data);
                return Result<string>.Failure(ErrorCode.LoadFailed, e.Message);
            }
        }

        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            return File.Exists(GetFilePath(key));
        }

        public Result<bool> Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return Result<bool>.Failure(ErrorCode.SaveFailed, "삭제 키가 비어있습니다.");
            }

            try
            {
                var filePath = GetFilePath(key);

                if (!File.Exists(filePath))
                {
                    return Result<bool>.Success(true); // 이미 없으면 성공 처리
                }

                File.Delete(filePath);
                Log.Debug($"[FileSaveStorage] 삭제 완료: {key}", LogCategory.Data);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                Log.Error($"[FileSaveStorage] 삭제 실패: {key} - {e.Message}", LogCategory.Data);
                return Result<bool>.Failure(ErrorCode.SaveFailed, e.Message);
            }
        }
    }
}
