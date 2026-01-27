---
name: verify
description: 컴파일 + 코드 리뷰 검증
---

# 검증 프로토콜

구현 완료 후 필수 검증을 수행합니다.

## 실행 단계

1. **컴파일 확인**: Unity Editor 로그에서 에러 확인
   - Windows: `$env:LOCALAPPDATA\Unity\Editor\Editor.log`
   - Mac: `~/Library/Logs/Unity/Editor.log`
   - 필터: Error, Exception

2. **변경 파일 파악**: git diff로 수정된 .cs 파일 목록 확인

3. **코드 리뷰**: Task(code-reviewer, model=opus)로 변경 파일 분석
   - high confidence 이슈만 보고

4. **결과 종합**: 검증 통과 여부 판정

## 검증 기준

### 컴파일
- Unity Editor 로그에 Error/Exception 없음

### 코드 리뷰
- 버그/로직 오류 없음
- 보안 취약점 없음
- 프로젝트 컨벤션 준수

## 결과 포맷

```
## 검증 결과

### 컴파일
- 상태: (성공/실패)
- 에러: (있으면 목록)

### 코드 리뷰
- 검토 파일: (개수)
- 이슈: (high confidence만)

### 결론
- (검증 통과 / 수정 필요)
```

## 검증 실패 시

1. 발견된 이슈 수정
2. `/verify` 재실행
3. 통과할 때까지 반복
