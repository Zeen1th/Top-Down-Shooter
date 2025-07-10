# Top-Down Shooter - CI Workflow

This Unity project uses GitHub Actions to automatically build the game for multiple platforms when changes are pushed to the repository.

## 🔧 CI Workflow Overview

The workflow is defined in `.github/workflows/unity.yml`. It runs automatically on every push.

### 🔁 What it does:
- **Builds the game for each supported platform:**
  - `StandaloneWindows64` (Windows)
  - `WebGL` (Web)
- **Caches the Unity `Library` folder** to speed up future builds.
- **Uses Unity Builder (game-ci)** to handle project builds.
- **Runs a Unity build using `BuildScript.PerformBuild()`**, located in `Assets/Editor/BuildScript.cs`.
- **Uploads each build as an artifact** so you can download and test it.

### 📁 Artifacts:
After each build, the result is available in the GitHub Actions tab under your workflow run. You’ll find downloadable builds for each platform.

## 🔐 Secrets Required

Make sure you have the following GitHub secrets set in your repo:

- `UNITY_LICENSE` – Your Unity license (text or base64 format).
- `UNITY_EMAIL` – Your Unity login email.
- `UNITY_PASSWORD` – Your Unity password or app password (if using 2FA).

Add these under **Settings > Secrets and variables > Actions** in your GitHub repo.

## 🔀 Branching Strategy

We use a simple and clean strategy:

- **`main`**: Stable builds only. Push or merge here only after testing.
- **Feature branches**: Use branches like `feature/powerups` or `bugfix/enemy-ai` for development. Merge to `main` through pull requests with reviews if working as a team.

This keeps the main branch safe and ensures CI builds pass before merging.

## ▶️ Running the Build Locally

To build the project from the command line:

```bash
unity -quit -batchmode -projectPath . -executeMethod BuildScript.PerformBuild
