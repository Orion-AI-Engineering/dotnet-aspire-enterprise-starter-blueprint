# Architecture

The starter uses a simple asynchronous flow.

- Web submits requests to API
- API stores request state and publishes `OrderRequested`
- Worker processes the message asynchronously
- API exposes read endpoints for status tracking

The structure keeps the demo small while still showing the seams you would keep in production.
