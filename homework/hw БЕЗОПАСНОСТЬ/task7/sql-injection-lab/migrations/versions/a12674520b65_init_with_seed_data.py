from alembic import op
import sqlalchemy as sa

def upgrade():
    op.create_table("users",
        sa.Column("id", sa.Integer(), primary_key=True),
        sa.Column("username", sa.String(50), unique=True, nullable=False),
        sa.Column("password", sa.String(255), nullable=False),
    )
    op.create_table("tokens",
        sa.Column("id", sa.Integer(), primary_key=True),
        sa.Column("user_id", sa.Integer(), sa.ForeignKey("users.id")),
        sa.Column("token", sa.String(255), unique=True, nullable=False),
    )
    op.create_table("orders",
        sa.Column("id", sa.Integer(), primary_key=True),
        sa.Column("user_id", sa.Integer(), sa.ForeignKey("users.id")),
        sa.Column("product", sa.String(100)),
        sa.Column("amount", sa.Integer()),
    )

    op.execute("""
        INSERT INTO users (username, password) VALUES
        ('alice', 'alicepass'), ('bob', 'bobpass'), ('admin', 'admin123')
    """)
    op.execute("""
        INSERT INTO tokens (user_id, token) VALUES
        (1, 'secrettokenAlice'), (2, 'secrettokenBob')
    """)
    op.execute("""
        INSERT INTO orders (user_id, product, amount) VALUES
        (1, 'Laptop', 1500), (1, 'Mouse', 50), (2, 'Keyboard', 120)
    """)

def downgrade():
    op.drop_table("orders")
    op.drop_table("tokens")
    op.drop_table("users")