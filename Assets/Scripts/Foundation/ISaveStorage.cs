namespace Sc.Foundation
{
    /// <summary>
    /// 저장소 추상화 인터페이스.
    /// 파일, 메모리, 클라우드 등 다양한 저장 방식 지원.
    /// </summary>
    public interface ISaveStorage
    {
        /// <summary>
        /// 데이터 저장
        /// </summary>
        /// <param name="key">저장 키</param>
        /// <param name="data">JSON 문자열</param>
        /// <returns>성공 여부</returns>
        Result<bool> Save(string key, string data);

        /// <summary>
        /// 데이터 로드
        /// </summary>
        /// <param name="key">저장 키</param>
        /// <returns>JSON 문자열 (없으면 실패 Result)</returns>
        Result<string> Load(string key);

        /// <summary>
        /// 데이터 존재 여부 확인
        /// </summary>
        bool Exists(string key);

        /// <summary>
        /// 데이터 삭제
        /// </summary>
        /// <param name="key">저장 키</param>
        /// <returns>성공 여부</returns>
        Result<bool> Delete(string key);
    }
}
