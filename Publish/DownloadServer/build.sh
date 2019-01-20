#!/bin/sh
cargo build --release
cp ./target/release/dlserver ./
cross build --target x86_64-pc-windows-gnu --release
cp ./target/x86_64-pc-windows-gnu/dlserver.exe ./
