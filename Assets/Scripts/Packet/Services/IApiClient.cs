using System;
using Cysharp.Threading.Tasks;
using Sc.Data;

namespace Sc.Packet
{
    /// <summary>
    /// API 클라이언트 인터페이스
    /// HTTP REST 통신 추상화
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// 클라이언트 초기화
        /// </summary>
        /// <param name="baseUrl">서버 기본 URL</param>
        UniTask<bool> InitializeAsync(string baseUrl);

        /// <summary>
        /// 요청 전송 (제네릭)
        /// </summary>
        /// <typeparam name="TRequest">요청 타입</typeparam>
        /// <typeparam name="TResponse">응답 타입</typeparam>
        /// <param name="request">요청 데이터</param>
        /// <returns>응답 데이터</returns>
        UniTask<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest
            where TResponse : IResponse;

        /// <summary>
        /// 요청 전송 (non-generic, 내부 큐에서 사용)
        /// 리플렉션 없이 요청 처리를 위한 메서드
        /// </summary>
        /// <param name="request">요청 데이터</param>
        /// <returns>응답 데이터</returns>
        UniTask<IResponse> SendAsync(IRequest request);

        /// <summary>
        /// 초기화 여부
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// 현재 세션 토큰
        /// </summary>
        string SessionToken { get; }
    }

    /// <summary>
    /// 요청 메타데이터
    /// </summary>
    public class RequestMetadata
    {
        /// <summary>
        /// 요청 ID (고유 식별자)
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 요청 타입 이름 (예: "Login", "Gacha")
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// 우선순위 (높을수록 먼저 처리, 기본 0)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 타임아웃 (밀리초, 기본 30000)
        /// </summary>
        public int TimeoutMs { get; set; } = 30000;

        /// <summary>
        /// 재시도 횟수 (기본 0)
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// 요청 생성 시간
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public static RequestMetadata Create<TRequest>() where TRequest : IRequest
        {
            return new RequestMetadata
            {
                RequestId = Guid.NewGuid().ToString(),
                RequestType = typeof(TRequest).Name.Replace("Request", ""),
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    /// <summary>
    /// 큐에 들어가는 요청 래퍼
    /// </summary>
    public class QueuedRequest
    {
        /// <summary>
        /// 요청 ID
        /// </summary>
        public string RequestId { get; }

        /// <summary>
        /// 원본 요청
        /// </summary>
        public IRequest Request { get; }

        /// <summary>
        /// 요청 메타데이터
        /// </summary>
        public RequestMetadata Metadata { get; }

        /// <summary>
        /// 큐에 추가된 시간
        /// </summary>
        public DateTime EnqueuedAt { get; }

        /// <summary>
        /// 요청 타입
        /// </summary>
        public Type RequestType { get; }

        /// <summary>
        /// 응답 타입
        /// </summary>
        public Type ResponseType { get; }

        public QueuedRequest(IRequest request, RequestMetadata metadata, Type requestType, Type responseType)
        {
            RequestId = metadata.RequestId;
            Request = request;
            Metadata = metadata;
            EnqueuedAt = DateTime.UtcNow;
            RequestType = requestType;
            ResponseType = responseType;
        }
    }
}
