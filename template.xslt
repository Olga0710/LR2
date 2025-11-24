<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:output method="html" encoding="UTF-8" indent="yes"/>

    <xsl:template match="/">
        <html>
        <head>
            <title>Електронний архів факультету</title>
            <style>
                table { border-collapse: collapse; width: 100%; font-family: Arial; }
                th, td { border: 1px solid #666; padding: 6px; text-align: left; }
                th { background-color: #efefef; }
            </style>
        </head>
        <body>
            <h2>Електронний архів факультету</h2>

            <table>
                <tr>
                    <th>ID</th>
                    <th>Автор</th>
                    <th>Назва матеріалу</th>
                    <th>Факультет</th>
                    <th>Кафедра</th>
                    <th>Вид матеріалу</th>
                    <th>Обсяг</th>
                    <th>Дата створення</th>
                </tr>

                <xsl:for-each select="archive/material">
                    <tr>
                        <td><xsl:value-of select="@id"/></td>
                        <td><xsl:value-of select="author"/></td>
                        <td><xsl:value-of select="title"/></td>
                        <td><xsl:value-of select="@faculty"/></td>
                        <td><xsl:value-of select="@department"/></td>

                        <td>
                            <xsl:choose>
                                <xsl:when test="@type">
                                    <xsl:value-of select="@type"/>
                                </xsl:when>
                                <xsl:otherwise>
                                    <xsl:value-of select="type"/>
                                </xsl:otherwise>
                            </xsl:choose>
                        </td>

                        <td><xsl:value-of select="size"/></td>
                        <td><xsl:value-of select="creationDate"/></td>
                    </tr>
                </xsl:for-each>

            </table>
        </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
