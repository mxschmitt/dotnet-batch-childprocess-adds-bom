console.log("hello from Node.js")

process.stdin.on('data', (chunk) => {
  console.log(`Received ${chunk.length} bytes of data.`);
  for (const byte of chunk) {
    console.log(`Received ${byte} (${String.fromCharCode(byte)})`);
    if (byte === 113 /* letter q */) {
      console.log("Exiting")
      process.exit()
    }
  }
})