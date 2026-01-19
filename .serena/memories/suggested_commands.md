# ProjectSC 개발 명령어

## Windows 시스템 명령어
- `dir` - 디렉토리 목록 (unix: ls)
- `type` - 파일 내용 출력 (unix: cat)
- `findstr` - 텍스트 검색 (unix: grep)
- `where` - 파일 위치 찾기 (unix: which)
- `cd` - 디렉토리 이동
- `mkdir` - 디렉토리 생성
- `copy`, `xcopy` - 파일 복사
- `del` - 파일 삭제
- `rmdir /s` - 디렉토리 삭제

## Git 명령어
- `git status` - 상태 확인
- `git add .` - 스테이징
- `git commit -m "메시지"` - 커밋
- `git push` - 푸시
- `git pull` - 풀
- `git log --oneline -10` - 최근 커밋 확인

## Unity Editor 메뉴 (개발 도구)
- `SC Tools/MVP/Create All` - MVP 씬/프리팹 자동 생성
- `SC Tools/SystemPopup` - 시스템 팝업 프리팹 생성
- `SC/Data/Master Data Generator` - JSON → SO 변환
- `SC Tools/Navigation Debug` - Navigation 상태 시각화
- `SC Tools/Data Flow Test` - 데이터 흐름 테스트
- `SC Tools/PlayMode Tests` - PlayMode 테스트 메뉴

## 테스트 실행
- Unity Test Runner 사용 (Window > General > Test Runner)
- EditMode 테스트: `Sc.Editor.Tests` Assembly
- PlayMode 테스트: `Sc.Tests` Assembly

## Python 스크립트 (데이터 파이프라인)
```bash
cd Tools
python export_master_data.py
```

## 빌드
- Unity에서 File > Build Settings 사용
