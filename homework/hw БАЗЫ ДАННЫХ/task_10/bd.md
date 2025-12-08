```mermaid
erDiagram
    ARTIST ||--o{ ALBUM : "1:N (primary_artist_id)"
    ALBUM ||--|| ALBUM_INFO : "1:1"
    ARTIST }o--o{ ALBUM_CONTRIBUTOR : "M:N (соавторы)"
    ALBUM  }o--o{ ALBUM_CONTRIBUTOR : ""