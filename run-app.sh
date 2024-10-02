#!/bin/bash

# Function to detect OS
detect_os() {
    case "$(uname -s)" in
        Darwin*)    echo "osx" ;;
        Linux*)     echo "linux" ;;
        CYGWIN*)    echo "win" ;;
        MINGW*)     echo "win" ;;
        *)          echo "unknown" ;;
    esac
}

# Detect the OS
OS=$(detect_os)
echo "Detected OS: $OS"

# Build and run backend
echo "Building and running backend..."
cd TaskBackend || exit
./gradlew build
docker build -t task-backend .
docker run -d -p 8080:8080 task-backend
cd ..

# Build frontend
echo "Building frontend for $OS..."
cd TaskTrackerFrontend || exit
case $OS in
    osx)
        RUNTIME="osx-x64"
        ;;
    linux)
        RUNTIME="linux-x64"
        ;;
    win)
        RUNTIME="win-x64"
        ;;
    *)
        echo "Unsupported OS for packaging. Using default runtime."
        RUNTIME="win-x64"
        ;;
esac

dotnet publish -c Release -r $RUNTIME --self-contained true

# Run frontend
echo "Running frontend..."
case $OS in
    osx|linux)
        ./bin/Release/net8.0/$RUNTIME/publish/TaskTrackerFrontend
        ;;
    win)
        ./bin/Release/net8.0/$RUNTIME/publish/TaskTrackerFrontend.exe
        ;;
    *)
        echo "Unsupported OS for running. Please run the executable manually."
        ;;
esac